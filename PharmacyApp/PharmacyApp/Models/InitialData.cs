using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PharmacyApp.Models
{
    public class InitialData
    {
        public static void InitializeDb(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<PharmacyContext>();
            InitializeCompany(context);
            InitializeInternationNameMedicine(context);
        }

        public static void InitializeCompany(PharmacyContext context)
        {
            if (!context.Companies.Any())
            {
                context.Companies.AddRange(
                    new Company()
                    {
                        Name = "Borimed",
                        Country = new Country()
                        {
                            Name = "Belarus"
                        }
                    },
                    new Company()
                    {
                        Name = "Himalaya",
                        Country = new Country()
                        {
                            Name = "India"
                        }
                    },
                    new Company()
                    {
                        Name = "Bayer",
                        Country = new Country()
                        {
                            Name = "Germany"
                        }
                    },
                    new Company()
                    {
                        Name = "Darnitsa",
                        Country = new Country()
                        {
                            Name = "Ukraine"
                        }
                    }
                );
                context.SaveChanges();
            }
        }

        public static void InitializeInternationNameMedicine(PharmacyContext context)
        {
            if (!context.InternationalNames.Any())
            {
                context.InternationalNames.AddRange(
                    new InternationalName()
                    {
                        Name = "Acetylsalicylic acid"
                    },
                    new InternationalName()
                    {
                        Name = "Afobazol"
                    },
                    new InternationalName()
                    {
                        Name = "Atgam"
                    },
                    new InternationalName()
                    {
                        Name = "Arbidol"
                    },
                    new InternationalName()
                    {
                        Name = "Calcium-D3 nycomed"
                    },
                    new InternationalName()
                    {
                        Name = "Paracetamol C Hemofarm"
                    },
                    new InternationalName()
                    {
                        Name = "Cardiomagnyl"
                    },
                    new InternationalName()
                    {
                        Name = "Fosicard"
                    },
                    new InternationalName()
                    {
                        Name = "Magnelis B6"
                    },
                    new InternationalName()
                    {
                        Name = "Saltos"
                    },
                    new InternationalName()
                    {
                        Name = "Gastal"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
