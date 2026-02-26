using Microsoft.UI.Xaml;
using System;

namespace app_derivadas_2_pro
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnCalcularClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se espera que ingreses: a, b, c, d (ej: 4, -6, 2, -1)
                string[] valores = InputFuncion.Text.Split(',');
                double a = double.Parse(valores[0]);
                double b = double.Parse(valores[1]);
                double c = double.Parse(valores[2]);
                double d = double.Parse(valores[3]);

                // 1. Derivada: f'(x) = (3a)x^2 + (2b)x + c
                BarDerivada.Message = $"f'(x) = {3 * a}x² + {2 * b}x + {c}";

                // 2. Punto de Inflexión: f''(x) = 6ax + 2b = 0  => x = -2b / 6a
                double xInflex = -(2 * b) / (6 * a);
                double yInflex = (a * Math.Pow(xInflex, 3)) + (b * Math.Pow(xInflex, 2)) + (c * xInflex) + d;
                BarInflexion.Message = $"X: {xInflex:F2}, Y: {yInflex:F2}";

                // 3. Concavidad
                if (a > 0)
                    BarConcavidad.Message = "Cóncava (-) a Convexa (+)";
                else
                    BarConcavidad.Message = "Convexa (+) a Cóncava (-)";

                // 4. Teorema de Rolle (Simplificado para el intervalo [-1, 1])
                double f_m1 = (a * Math.Pow(-1, 3)) + (b * Math.Pow(-1, 2)) + (c * -1) + d;
                double f_1 = (a * Math.Pow(1, 3)) + (b * Math.Pow(1, 2)) + (c * 1) + d;

                if (Math.Abs(f_m1 - f_1) < 0.1)
                    BarRolle.Message = "Se cumple en [-1, 1] (f(a) ≈ f(b))";
                else
                    BarRolle.Message = "No se cumple f(a)=f(b) en [-1, 1]";

                // 5. Máximos y Mínimos (BarExtremos)
                // Usamos la fórmula general en la primera derivada: 3ax^2 + 2bx + c = 0
                double A = 3 * a;
                double B = 2 * b;
                double C = c;

                double discriminante = Math.Pow(B, 2) - (4 * A * C);

                if (discriminante > 0)
                {
                    double x1 = (-B + Math.Sqrt(discriminante)) / (2 * A);
                    double x2 = (-B - Math.Sqrt(discriminante)) / (2 * A);
                    BarExtremos.Message = $"Extremos en X1: {x1:F2} y X2: {x2:F2}";
                }
                else if (discriminante == 0)
                {
                    double xUnico = -B / (2 * A);
                    BarExtremos.Message = $"Punto crítico único en X: {xUnico:F2}";
                }
                else
                {
                    BarExtremos.Message = "No tiene máximos ni mínimos relativos reales";
                }
            }
            catch (Exception)
            {
                BarDerivada.Message = "Error: Ingresa formato 'a,b,c,d' (Ej: 4,-6,2,-1)";
                BarInflexion.Message = "---";
                BarConcavidad.Message = "---";
                BarRolle.Message = "---";
                BarExtremos.Message = "---";
            }
        }
    }
}