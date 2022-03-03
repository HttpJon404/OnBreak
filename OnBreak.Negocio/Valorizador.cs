using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Valorizador
    {
        public int Asistentes { get; set; }
        public int PersonalAdicional { get; set; }

        public Valorizador()
        {
            this.Init();
        }

        public void Init()
        {

        }

        public double CalcularCoffeBreak(int valorBase,int asistentes, int perAdicional)
        {
            double recargoA=0;
            double recargoB; ;

            double totalUF;

            if (asistentes>=1 && asistentes<=20)
            {
				recargoA=3;
			}
		    if(asistentes>=21 && asistentes<=50)
			{
				recargoA=5;
			}
            if (asistentes>50)
            {
                recargoA = Math.Ceiling((double)(asistentes) / (double)(20)) * 2;
            }
			
				
            

            switch (perAdicional)
            {
                case 2:
                    recargoB = 2;
                    break;

                case 3:
                    recargoB = 3;
                    break;
                case 4:
                    recargoB = 3.5;
                    break;

                default:
                    recargoB = 3.5 + (perAdicional - 4) * 0.5;
                    break;
            }

            totalUF = valorBase+recargoA + recargoB;
            return totalUF;

        }

        public double CalcularCocktail(int valorBase, int asistentes, int perAdicional,int ambientacion, double musica)
        {
            double recargoA = 0;
            double recargoB; ;

            double totalUF;

            if (asistentes >= 1 && asistentes <= 20)
            {
                recargoA = 4;
            }
            if (asistentes >= 21 && asistentes <= 50)
            {
                recargoA = 6;
            }
            if (asistentes > 50)
            {
                recargoA = Math.Ceiling((double)(asistentes) / (double)(20)) * 2;
            }
            
            //Personal adicional.
            switch (perAdicional)
            {
                case 2:
                    recargoB = 2;
                    break;

                case 3:
                    recargoB = 3;
                    break;
                case 4:
                    recargoB = 3.5;
                    break;

                default:
                    recargoB = 3.5 + (perAdicional - 4) * 0.5;
                    break;
            }

            totalUF = valorBase + recargoA + recargoB+ambientacion+musica;
            return totalUF;
        }

        public double CalcularCenas(int valorBase, int asistentes, int perAdicional, double ambientacion, double musica,double local)
        {
            double recargoA = 0;
            double recargoB; ;

            double totalUF;

            if (asistentes >= 1 && asistentes <= 20)
            {
                recargoA=asistentes*1.5;
            }
            if (asistentes >= 21 && asistentes <= 50)
            {
                recargoA = asistentes * 1.2;
            }
            if (asistentes > 50)
            {
                recargoA = asistentes * 1;
            }

            //Personal adicional.
            switch (perAdicional)
            {
                case 2:
                    recargoB = 3;
                    break;

                case 3:
                    recargoB = 4;
                    break;
                case 4:
                    recargoB = 5;
                    break;

                default:
                    recargoB = 5 + (perAdicional - 4) * 0.5;
                    break;
            }
            totalUF = valorBase + recargoA + recargoB + ambientacion + musica+local;

            return totalUF;
        }

    }     
}

