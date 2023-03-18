using Certificados.Domain.Dto.Responses;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Reflection;
using Document = Microsoft.Office.Interop.Word.Document;
using Word = Microsoft.Office.Interop.Word;

namespace Certificados.Util
{
    public class WordHelper
    {
        private Object missing = System.Reflection.Missing.Value;
        private readonly int numberMaxOfRow = 36;
    
        public string GerarPDFDeWord(Object objeto, string arquivo)
        {
            string tempFile = SetValuesInTemplate(objeto, arquivo);
            string pdfPath = GeneratePdf(objeto, tempFile);
            // var pdf = File.ReadAllBytes(pdfPath);
            DeleteTempFile(tempFile);
            return pdfPath;
        }

        public byte[] GerarWord(Object objeto, string arquivo)
        {
            string tempFile = SetValuesInTemplate(objeto, arquivo);
            var word = File.ReadAllBytes(tempFile);
            File.Delete(tempFile);
            return word;
        }

        private string GeneratePdf(Object objeto, string tempFile)
        {
            Word.Application word = new Word.Application();

            Object oMissing = System.Reflection.Missing.Value;

            word.Visible = false;
            word.ScreenUpdating = false;

            Document doc = word.Documents.Open(tempFile, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            // Object outputFileName = tempFile.Replace(".docx", ".pdf");




            string[] path = tempFile.Split("++");

            string pdfFileName = path[0] + "_" + ((Certificado)objeto).Segurado.Cpf.Replace(".", "").Replace("-", "") + "_" + ((Certificado)objeto).NumeroCertificado.Replace(".", "").Replace("-", "") + ".pdf";
            Object outputFileName = pdfFileName;


            Object fileFormat = Word.WdSaveFormat.wdFormatPDF;

            /* LOGS*/
            //  Logger.Error($"Temp do doc :: {tempFile}");
            // Logger.Error($"Temp do pdf :: {outputFileName}");
            /// if (doc == null) Logger.Error($"Doc é null"); 



            doc.SaveAs(ref outputFileName,
                ref fileFormat, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            ((Word._Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);

            ((Word._Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);



            GC.ReRegisterForFinalize(word);
            GC.Collect();

            word = null;

            return outputFileName.ToString();
        }

        private void DeleteTempFile(string wordDoc)
        {

            File.Delete(wordDoc);
            var pdfDoc = wordDoc.Replace(".docx", ".pdf");
            File.Delete(pdfDoc);
        }

        public void RenameMergeField(Word.Application oWord, Document doc, string oldName, string newName)
        {
            /*
             * Usado com a biblioteca interop
             * */
            foreach (Word.MailMergeField field in doc.MailMerge.Fields)
            {
                if (field.Code.Text.Contains(oldName))
                {
                    //field.Code.Text = field.Code.Text.Replace(oldName, newName);
                    field.Select();
                    oWord.Selection.TypeText(newName);
                }
            }
        }

        private void SetValues(Word.Application oWord, Document oWordDoc, Object objeto, string nomeObjeto = "")
        {
            /*
             * Usado com a biblioteca interop
             * */
            nomeObjeto += objeto.GetType().Name + ".";

            foreach (PropertyInfo prop in objeto.GetType().GetProperties())
            {
                string propertyName = prop.Name;
                var propertyValue = prop.GetValue(objeto);

                if (IsPrimitiveType(prop.PropertyType.Name))
                {
                    string oldValue = nomeObjeto + propertyName;
                    RenameMergeField(oWord, oWordDoc, oldValue, propertyValue.ToString());
                }
                else
                {
                    SetValues(oWord, oWordDoc, propertyValue, nomeObjeto);
                }
            }
        }

        private void SetValues(WordprocessingDocument document, IEnumerable<FieldCode> fields, Object objeto, string nomeObjeto = "")
        {
            nomeObjeto += objeto.GetType().Name + ".";

            foreach (PropertyInfo prop in objeto.GetType().GetProperties())
            {
                string propertyName = prop.Name;
                var propertyValue = prop.GetValue(objeto);

                if (IsPrimitiveType(prop.PropertyType.Name))
                    ReplaceMergeFieldWithText(fields, $"{nomeObjeto}{propertyName}", PrintValue(propertyValue, prop.PropertyType.Name));
                else if (IsArrayList(prop.PropertyType.Name))
                    AddRowInTable(document, new List<Object>((IEnumerable<Object>)propertyValue), propertyName);
                else if (propertyValue != null)
                    SetValues(document, fields, propertyValue, nomeObjeto);
            }
        }

        private string PrintValue(Object propertyValue, string typeName)
        {
            return typeName == "DateTime" ? ((DateTime)propertyValue).ToString("dd/MM/yyyy") : propertyValue?.ToString();
        }

        private bool IsPrimitiveType(string typeName)
        {
            return typeName == "String" || typeName == "Decimal" || typeName == "Int32" || typeName == "DateTime";
        }

        private bool IsArrayList(string typeName)
        {
            return typeName.Contains("List");
        }

        private string SetValuesInTemplate(Object objeto, string arquivo)
        {
            string pathTemplate = $@"{Directory.GetCurrentDirectory()}\Assets\Docs\{arquivo}.docx";
            string targetDir = $@"{Directory.GetCurrentDirectory()}\Temp";
            string targetFile = $@"{targetDir}\{arquivo}++{Guid.NewGuid().ToString()}.docx";

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }


            File.Copy(pathTemplate, targetFile, true);

            using (WordprocessingDocument document = WordprocessingDocument.Open(targetFile, true))
            {
                document.ChangeDocumentType(WordprocessingDocumentType.Document);

                MainDocumentPart mainPart = document.MainDocumentPart;
                var mergeFields = mainPart.RootElement.Descendants<FieldCode>();

                SetValues(document, mergeFields, objeto);
                mainPart.Document.Save();
            }

            return targetFile;
        }

        private void ReplaceMergeFieldWithText(IEnumerable<FieldCode> fields, string mergeFieldName, string replacementText)
        {
            var FoundFields = fields.Where(f => f.InnerText.Contains(mergeFieldName)).ToList();

            FoundFields.ForEach(field =>
            {
                Run rFldCode = (Run)field.Parent;

                Run rBegin = rFldCode.PreviousSibling<Run>();
                Run rSep = rFldCode.NextSibling<Run>();
                Run rText = rSep.NextSibling<Run>();
                Run rEnd = rText.NextSibling<Run>();

                Text t = rText.GetFirstChild<Text>();
                t.Text = replacementText;

                rFldCode.Remove();
                rBegin.Remove();
                rSep.Remove();
                rEnd.Remove();
            });
        }

        private void AddRowInTable(WordprocessingDocument doc, IList<Object> list, string tableName)
        {
            Body bod = doc.MainDocumentPart.Document.Body;
            var tables = bod.Descendants<Table>();
            var table = tables.FirstOrDefault(x => ((TableProperties)x.FirstChild).TableDescription?.Val.Value == tableName);

            if (table == null) return;

            var rowToInsert = GetRowToInsert(table);
            var listOfCellsInRow = GetColumnsOfRow(rowToInsert);
            var properties = GetPropertiesFromList(list);

            if (list == null || list.Count == 0)
                SetEmptyRow(table, listOfCellsInRow, rowToInsert);
            else
                SetRows(table, list, listOfCellsInRow, properties, rowToInsert);
        }

        private void SetEmptyRow(Table table, List<TableCell> listOfCellsInRow, TableRow rowToInsert)
        {
            for (int j = 0; j < listOfCellsInRow.Count(); j++)
            {
                listOfCellsInRow[j].Descendants<Text>().FirstOrDefault().Text = String.Empty;
            }
            table.Descendants<TableRow>().Last().InsertAfterSelf(rowToInsert.CloneNode(true));
        }

        private void SetRows(Table table, IList<Object> list, List<TableCell> listOfCellsInRow, PropertyInfo[] properties, TableRow rowToInsert)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (i >= numberMaxOfRow) break; // Controle para que o numero de linhas da tabela não quebre o layout.

                for (int j = 0; j < listOfCellsInRow.Count(); j++)
                {
                    var propertyValue = properties[j].GetValue(item).ToString();
                    listOfCellsInRow[j].Descendants<Text>().FirstOrDefault().Text = propertyValue;
                }
                table.Descendants<TableRow>().Last().InsertAfterSelf(rowToInsert.CloneNode(true));

                i++;
            }
        }

        private TableRow GetRowToInsert(Table table)
        {
            var rows = table.Descendants<TableRow>().ToList();
            var myRow = (TableRow)rows.Last().Clone();
            return (TableRow)myRow.Clone();
        }

        private List<TableCell> GetColumnsOfRow(TableRow row)
        {
            return row.Descendants<TableCell>().ToList();
        }

        private PropertyInfo[] GetPropertiesFromList(IList<Object> list)
        {
            return (list == null || list.Count == 0)
                ? null : list[0].GetType().GetProperties();
        }
    }
}
