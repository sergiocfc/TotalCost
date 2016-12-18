namespace TotalCost.Lib.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TotalCost.UI.Lib;

    internal sealed class Configuration : DbMigrationsConfiguration<TotalCost.UI.Lib.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TotalCost.UI.Lib.Context";
        }

        protected override void Seed(Context c)
        {
            
            if (c.Groups.Count() == 0)
            {
                var groups = new Group[]
                {
                        new Group
                        {
                            Name = "Base",
                            Type = GroupType.Income,
                        },
                        new Group
                        {
                            Name = "TransferFrom",
                            Type = GroupType.Consumption
                        },
                        new Group
                        {
                            Name = "TransferFrom",
                            Type = GroupType.Income
                        },
                        new Group
                        {
                            Name = "Продукты",
                            Type = GroupType.Consumption,
                            Icon = "clothes.png"
                        },
                        new Group
                        {
                            Name = "Одежда",
                            Type = GroupType.Consumption,
                            Icon = "clothes.png"
                        },
                        new Group
                        {
                            Name = "Проезд",
                            Type = GroupType.Consumption,
                            Icon = "transport.png"
                        },
                        new Group
                        {
                            Name = "Стипендия",
                            Type = GroupType.Income,
                            Icon = "scholarship.png"
                        },
                        new Group
                        {
                            Name = "Зарплата",
                            Type = GroupType.Income,
                            Icon = "wage.png"
                        }
                };
                c.Groups.AddRange(groups);
            }

            if (c.Bills.Count() == 0)
            {
                var b = new Bill
                {
                    Name = "Кошелёк",
                    Type = BillType.Cash
                };
                c.Bills.Add(b);
            }

            c.SaveChanges();
        }
    }
}
