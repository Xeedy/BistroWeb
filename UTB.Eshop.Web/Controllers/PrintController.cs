using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using BistroWeb.Application.Abstraction;
using System.Linq;
using WordText = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace BistroWeb.Web.Controllers
{
    public class PrintController : Controller
    {
        private readonly IProductAppService _productAppService;

        public PrintController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        public IActionResult Print()
        {
            var products = _productAppService.GetActiveProducts();

            if (products == null || !products.Any())
            {
                return NotFound("No products available.");
            }

            var breweries = products
                .GroupBy(p => p.Breweries?.Name ?? "Unknown Brewery")
                .OrderBy(g => g.Key)
                .ToList();

            using (var mem = new MemoryStream())
            {
                using (var wordDoc = WordprocessingDocument.Create(mem, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                {
                    var mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document();

                    // Nastavení okrajů stránky
                    SectionProperties sectionProps = new SectionProperties();
                    PageMargin pageMargin = new PageMargin()
                    {
                        Top = 0, // 0 cm v Twips
                        Right = 1417, // 2,5 cm v Twips (1 cm = 567 Twips)
                        Bottom = 0, // 0 cm v Twips
                        Left = 1417, // 2,5 cm v Twips
                        Header = 0,
                        Footer = 0,
                        Gutter = 0
                    };
                    sectionProps.Append(pageMargin);

                    var body = mainPart.Document.AppendChild(new Body());
                    mainPart.Document.Body.Append(sectionProps);
                    // Nadpis dokumentu
                    var titleParagraph = new Paragraph(
                        new ParagraphProperties(
                            new Justification { Val = JustificationValues.Center },
                            new SpacingBetweenLines { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto } // Nastavení mezer mezi řádky
                        ),
                        new Run(
                            new RunProperties(
                                new Bold(),
                                new Underline { Val = UnderlineValues.Single },
                                new RunFonts { Ascii = "Arial Black" },
                                new FontSize { Val = "44" } // Velikost písma 22pt (44 je v polovičních bodech)
                            ),
                            new WordText("Lahvová piva:") // Zajištění správné diakritiky
                        )
                    );
                    body.AppendChild(titleParagraph);

                    foreach (var breweryGroup in breweries)
                    {
                        // Nadpis pivovaru
                        var breweryNameParagraph = new Paragraph(
                            new ParagraphProperties(
                                new SpacingBetweenLines { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto } // Nastavení mezer mezi řádky
                            ),
                            new Run(
                                new RunProperties(
                                    new Bold(),
                                    new RunFonts { Ascii = "Arial" },
                                    new FontSize { Val = "24" } // Velikost písma 12pt
                                ),
                                new WordText($"{breweryGroup.Key}:")
                            )
                        );
                        body.AppendChild(breweryNameParagraph);

                        foreach (var product in breweryGroup)
                        {
                            // Detaily produktu
                            var productDetailsParagraph = new Paragraph(
                                new ParagraphProperties(
                                    new Justification { Val = JustificationValues.Both },
                                    new Tabs(
                                        new TabStop { Val = TabStopValues.Right, Position = 9000 } // Nastavení pravého zarovnání
                                    ),
                                    new SpacingBetweenLines { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto } // Nastavení mezer mezi řádky
                                ),
                                new Run(
                                    new RunProperties(
                                        new RunFonts { Ascii = "Arial" },
                                        new FontSize { Val = "24" } // Velikost písma 12pt
                                    ),
                                    new WordText($"{product.Name}, {product.Typees?.Name}")
                                ),
                                new Run(new TabChar()), // Tabulátor pro zarovnání ceny
                                new Run(
                                    new RunProperties(
                                        new RunFonts { Ascii = "Arial" },
                                        new FontSize { Val = "24" } // Velikost písma 12pt
                                    ),
                                    new WordText($"{product.Price},-")
                                )
                            );

                            body.AppendChild(productDetailsParagraph);
                        }

                        // Přidání prázdného odstavce pro mezery mezi pivovary
                        body.AppendChild(new Paragraph(new Run(new WordText(""))));
                    }

                    mainPart.Document.Save();
                }

                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                string fileName = $"piva_{currentDate}.docx";
                byte[] bytes = mem.ToArray();

                return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
            }
        }
    }
}
