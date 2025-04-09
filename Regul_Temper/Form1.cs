using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regul_Temper
{
    public partial class Form1 : Form
    {
        public double dT(double v, double ro, double cp, double F, double To, double Tr, double Q)
        {
            double r = (F / v) * (To - Tr) + Q / (ro * cp * v);
            return r;
        }
        public double dQ(double v, double ro, double cp, double F, double To, double Tr, double kp, double tau, double Tz, double Q)
        {
            double r = -kp * ((F / v) * (To - Tr) + Q / (ro * cp * v)) + 
                kp * (Tz / Tr) / tau;
            return r;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double ro = 1;
            double cp = 1;
            double Tz = 80;
            double kp = 1;
            double tau = 0.5;
            double F = 10;
            double v = 100;

            double h = 0.1; // шаг по времени
            double tn = 0;
            int nt = 100;

            double[] t = new double[500];
            double[] Tout = new double[500];
            double[] Qout = new double[500];
            
            double To = 25;
            double Qo = 0;

            t[1] = tn;
            Tout[1] = To;
            Qout[1] = Qo;
            for (int i = 2; i <= nt; i++)
            {
                t[i] = t[i - 1] + h;
                // Метод Эйлера для интегрирования обыкновенных дифференциальных уравнений
                double dti = dT(v, ro, cp, F, To, Tout[i - 1], Qout[i - 1]);
                double dqi = dQ(v, ro, cp, F, To, Tout[i - 1], kp, tau, Tz, Qout[i - 1]);

                Tout[i] = Tout[i - 1] + h * dti;
                Qout[i] = Qout[i - 1] + h * dqi;
            }
        }
    }
}
