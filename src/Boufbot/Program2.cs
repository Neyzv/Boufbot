// using System;
// using System.Drawing;
// using System.Drawing.Imaging;
// using Tesseract;
// using ImageFormat = System.Drawing.Imaging.ImageFormat;
//
// namespace TesseractSharp;
//
// class Program2
// {
//     static void ddddd(string[] args)
//     {
//         Console.WriteLine("Chemin de l'image :");
//         string imagePath = Console.ReadLine().Trim('"');
//
//         string tessDataPath = "./tessdata";
//
//         try
//         {
//             using (Bitmap original = new Bitmap(imagePath))
//             {
//                 // 1) Prétraitement : extraction du texte jaune
//                 Bitmap preprocessed = PreprocessImage(ResizeImage(FindMinXForYellow(original), 1.5));
//
//                 // 3) Debug
//                 preprocessed.Save("debug_preprocessed.png", ImageFormat.Png);
//
//                 // 4) OCR final
//                 using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
//                 {
//                     engine.SetVariable("tessedit_char_whitelist",
//                         "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz’'-:%éèàùêôç0123456789");
//
//                     using (var page = engine.Process(preprocessed))
//                     {
//                         Console.WriteLine("🎯 Texte OCR :");
//                         Console.WriteLine(page.GetText());
//                     }
//                 }
//
//                 preprocessed.Dispose();
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("❌ Erreur : " + ex.Message);
//         }
//     }
//     
//     static Bitmap FindMinXForYellow(Bitmap img)
//     {
//         int minX = img.Width; // on initialise au maximum possible
//
//         Color targetYellow = Color.FromArgb(240, 255, 79); // #F0FF4F
//         Color targetYellow2 = Color.FromArgb(255, 209, 148);
//         int tol = 1; // tolérance si nécessaire
//
//         for (int y = 0; y < img.Height; y++)
//         {
//             for (int x = 0; x < img.Width; x++)
//             {
//                 Color c = img.GetPixel(x, y);
//
//                 bool isYellow = 
//                     Math.Abs(c.R - targetYellow.R) <= tol &&
//                     Math.Abs(c.G - targetYellow.G) <= tol &&
//                     Math.Abs(c.B - targetYellow.B) <= tol;
//
//                 bool isYellow2 = 
//                     Math.Abs(c.R - targetYellow2.R) <= tol &&
//                     Math.Abs(c.G - targetYellow2.G) <= tol &&
//                     Math.Abs(c.B - targetYellow2.B) <= tol;
//
//                 if (isYellow || isYellow2)
//                 {
//                     if (x < minX)
//                         minX = x;
//                 }
//             }
//         }
//
//         minX -= 5;
//         
//         Rectangle cropRect = new Rectangle(minX, 0, img.Width - minX, img.Height);
//         Bitmap cropped = img.Clone(cropRect, img.PixelFormat);
//
//         return cropped;
//     }
//
//
//
//     // ---------------------------------------------------------------
//     // 1️⃣ Détection des couleurs exactes du texte Dofus
//     // ---------------------------------------------------------------
//     static bool IsDofusTextYellow(Color c)
//     {
//         var yellow1 = (R: 255, G: 209, B: 148); // #FFD194
//         var yellow2 = (R: 240, G: 255, B: 79);  // #F0FF4F
//         var yellow3 = (R: 205, G: 169, B: 122);  // #F0FF4F
//         var yellow4 = (R: 188, G: 200, B: 78);  // #F0FF4F
//
//         int tol = 30; // tolérance prête à l'emploi
//
//         bool nearYellow1 =
//             Math.Abs(c.R - yellow1.R) < tol &&
//             Math.Abs(c.G - yellow1.G) < tol &&
//             Math.Abs(c.B - yellow1.B) < tol;
//
//         bool nearYellow2 =
//             Math.Abs(c.R - yellow2.R) < tol &&
//             Math.Abs(c.G - yellow2.G) < tol &&
//             Math.Abs(c.B - yellow2.B) < tol;
//
//         bool nearYellow3 =
//             Math.Abs(c.R - yellow3.R) < tol &&
//             Math.Abs(c.G - yellow3.G) < tol &&
//             Math.Abs(c.B - yellow3.B) < tol;
//
//         bool nearYellow4 =
//             Math.Abs(c.R - yellow4.R) < tol &&
//             Math.Abs(c.G - yellow4.G) < tol &&
//             Math.Abs(c.B - yellow4.B) < tol;
//
//         return (nearYellow1 || nearYellow2 || nearYellow3 || nearYellow4) && c.B > 50;
//     }
//
//
//
//     // ---------------------------------------------------------------
//     // 2️⃣ Prétraitement : garder le texte jaune en blanc, reste en noir
//     // ---------------------------------------------------------------
//     static Bitmap PreprocessImage(Bitmap img)
//     {
//         Bitmap result = new Bitmap(img.Width, img.Height);
//
//         for (int y = 0; y < img.Height; y++)
//         {
//             for (int x = 0; x < img.Width; x++)
//             {
//                 Color c = img.GetPixel(x, y);
//
//                 if (IsDofusTextYellow(c))
//                     result.SetPixel(x, y, Color.Black);
//                 else
//                     result.SetPixel(x, y, Color.White);
//             }
//         }
//
//         return result;
//     }
//     
//     static Bitmap ResizeImage(Bitmap src, double scale)
//     {
//         int newWidth = (int)(src.Width * scale);
//         int newHeight = (int)(src.Height * scale);
//
//         Bitmap dest = new Bitmap(newWidth, newHeight);
//
//         using (Graphics g = Graphics.FromImage(dest))
//         {
//             g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
//             g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
//             g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
//             g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
//
//             g.DrawImage(src, 0, 0, newWidth, newHeight);
//         }
//
//         return dest;
//     }
//
//
//
//     // ---------------------------------------------------------------
//     // 3️⃣ ThickenText : épaissir le texte (OCR + fiable)
//     // ---------------------------------------------------------------
//     static Bitmap ThickenText(Bitmap img)
//     {
//         Bitmap result = new Bitmap(img.Width, img.Height);
//         
//         for (int y = 1; y < img.Height - 1; y++)
//         {
//             for (int x = 1; x < img.Width - 1; x++)
//             {
//                 if (img.GetPixel(x, y).R == 255) // pixel noir 
//                 {
//                     int whiteNeighbors = 0;
//
//                     // Voisins 4-connexes
//                     if (img.GetPixel(x - 1, y).R == 255) whiteNeighbors++;
//                     if (img.GetPixel(x + 1, y).R == 255) whiteNeighbors++;
//                     if (img.GetPixel(x, y - 1).R == 255) whiteNeighbors++;
//                     if (img.GetPixel(x, y + 1).R == 255) whiteNeighbors++;
//
//                     if (whiteNeighbors >= 1)
//                     {
//                         result.SetPixel(x - 1, y, Color.White);
//                         result.SetPixel(x + 1, y, Color.White);
//                         result.SetPixel(x, y - 1, Color.White);
//                         result.SetPixel(x, y + 1, Color.White);
//                     }
//                 }
//                 
//                 result.SetPixel(x, y, img.GetPixel(x, y));
//             }
//         }
//
//         return result;
//     }
// }

