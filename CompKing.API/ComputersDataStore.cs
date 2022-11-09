using CompKing.API.Models;

namespace CompKing.API
{
    public class ComputersDataStore
    {
        public List<ComputerDto> computers { get; set; }

        //public static ComputersDataStore Current { get; } = new ComputersDataStore();

        public ComputersDataStore()
        {
            computers = new List<ComputerDto>()
            {
                new ComputerDto()
                {
                    Id = 1,
                    Name = "Dell",
                    Description = "A black 27`` workstation.",
                    Components = new List<ComponentDto>()
                    {
                        new ComponentDto()
                        {
                            Id = 2,
                            Name = "ASUS Mini ATX B550 Motherboard",
                            Description = "Get the latest mini ATX motherboard for your workstation"
                        },
                        new ComponentDto()
                        {
                            Id = 3,
                            Name = " Kingston DDR 4 8gb rgb ram",
                            Description = "Kingston FURY™ Infrared Sync Technology"
                        }
                    }
                },
                new ComputerDto()
                {
                    Id = 2,
                    Name = "Hp",
                    Description = "A Silver 27`` workstation.",
                    Components = new List<ComponentDto>()
                    {
                        new ComponentDto()
                        {
                            Id = 3,
                            Name = "ASUS Micro ATX B550 Motherboard",
                            Description = "Get the latest micro ATX motherboard for your workstation"
                        },
                        new ComponentDto()
                        {
                            Id = 4,
                            Name = "Kingston 32GB 3600MHz DDR4 CL18 DIMM FURY Renegade Black",
                            Description = "Fierce black aluminium heat spreader"
                        }
                    }
                },
                new ComputerDto()
                {
                    Id = 3,
                    Name = "Samsung",
                    Description = "A silver 24`` workstation.",
                    Components = new List<ComponentDto>()
                    {
                        new ComponentDto()
                        {
                            Id = 3,
                            Name = "ASUS ATX B550 Motherboard",
                            Description = "Get the latest standard ATX motherboard for your workstation"
                        },
                        new ComponentDto()
                        {
                            Id = 5,
                            Name = "Corsair VENGEANCE RGB PRO 16GB (2 x 8GB) DDR4 3200MHz Kit - Black",
                            Description = "CORSAIR VENGEANCE RGB PRO Series DDR4 overclocked memory lights up your PC with mesmerizing dynamic multi-zone RGB lighting, while delivering the best in DDR4 performance."
                        }
                    }
                }
            };
        }
    }
}
