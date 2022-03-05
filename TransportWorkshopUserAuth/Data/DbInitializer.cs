using System;
using System.Linq;
using TransportWorkshopUserAuth.Models;
using TransportWorkshopUserAuth.Data;

namespace TransportWorkshopUserAuth.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            Random random = new Random();

            context.Database.EnsureCreated();

            if (context.AutoCars.Any() || context.Balances.Any() || context.Devices.Any() || context.Drivers.Any() || context.Maintenances.Any() || context.NormaFuels.Any()
                  || context.Tires.Any() || context.Trailers.Any() || context.TypeAutos.Any() || context.TypeFuels.Any() || context.WinterTimes.Any())
            {
                return;
            }

            int customersNumber = 20;  //количество машин и водителей                
            int deviceNumber = 20;     //количество устройств              
            int driverNumber = 20;     //водители
            int trailerNumber = 10;    //прицепы
            int typeOfworkNumber = 20; //топливо               
            int listTireNumber = 20;   //покрышки на всю технику
            int positionAuto = 10;   //типы автомобилей
            int typeFuel = 5;       //тип топлива

            string[] customersNameAuto = { "ГАЗ-3309", "МАЗ-4570", "КАМАЗ-353215", "МАЗ-5925А2", "ГАЗ-3309", "ЗИЛ-433362", "Volkswagen", "ГАЗ-3307",  "МТЗ-82",
                                            "ЗИЛ-130", "ГАЗ-66", "Амкодор-33", "ВАЗ-21310", "МАЗ-5551", "ВАЗ-21310","КАВЗ-3976","СРСД 25","БеларуС 320","ЗИЛ-433362","Skoda Octavia"};//модели машин 20
            string[] customersFirsLastMidName = { "Радьков А.Н.", "Серков И.И.", "Михайловский А.В.", "Музыченко А.А.", "Свириденко Д.Н.", "Устинович С.А.", "Шевкопляс В.А.",
                                                    "Кот Д.А.", "Кашкин А.М.", "Северин А.Н.", "Галуза Р.М.","Евсеенко И.И.","Лазебный А.В.","Цитриков А.А.","Винник М.Г.","Умрихин  С.Е.",
                                                    "Горбунов М.К.", "Григорьев Е.С.","Романов М.О","Федоренко С.К"};                                                             //водители 20

            string[] customersNumberAuto = { "AA 9363-3", "AE 3348-3", "AA 4614-3", "AK 3348-3", "ME 6342-3", "AM 6203-3", "AK 7031-3", "AB 6964-3", "CK 3294-3", "AM 4096-3",
                                                "AB 4565-3", "AA 8664-3", "AE 5894-3", "US 7564-3", "AD 2364-3", "BK 9308-3", "AS 6173-3", "AM 3690-3", "AA 5064-3", "AE 3901-3"};//номера машин 20
            string[] deviceData = { "14.01.2020", "17.02.2019", "14.05.2019", "18.08.2019", "18.10.2018", "19.10.2020","10.10.2019","01.10.2019","08.10.2019","13.01.2020",
                                        "20.11.2019", "25.09.2019","17.12.2017","11.11.2017","24.11.2017","07.11.2018","03.01.2017","20.12.2019","19.12.2020","26.09.2019"};//даты 20

            string[] nameFuel = { "ДТл", "ДТз", "А92", "А95","Газ" };                                                                                                        //типы топливо 4
            string[] customersTO = { "ТО1", "ТО2", "ТО3", "ТО4", "ТО5", "ТО6", "ТО7", "ТО8", "ТО9", "Т10" };                                                                       //типы ТО 10
            string[] nameDevice = { "МТЗ-82", "АСБ-300", "КС-3574", "2Д-12", "KioDP50C", "ПМ-130Б", "ПКС-5", "DRT80C", "ИЛ-980", "ЭО-3322", "УДС-114А", "DRT80C", "KO-512", "KO-503B2", "Отоп30Б" };//устройства 15
            string[] rightNumbers = { "ЕА 210763", "ЗАА 1562035", "ЗАА 310569", "ЗАА 049264", "ЗАА 090017", "ЕА 134146", "ЗАА 088144", "ЕА 211843", "ЕА 114529", "ЗАА 055842",
                                        "АГ 293695", "ЗАА 055235", "ЕА 211843","3АА 024715","ЕА 109122","ЕА 259031","ЕА 116019","ЗАА 046374","ЗАА 405673","ЗАА 052631" };      //права 20
            string[] descriptionsTires = { "195/65R15 91T", "205/55R16 94H", "185/60R14", "225/45R17", "320R508", "260R508", "175/80R16C", "425/85R21", "385/65R22.5", "400/70-21" };//типы покрышек 10
            string[] nameTires = { "Belshina", "Michelin", "Goodyear", "Continental", "Hankook", "Tigar", "Sava", "BFGoodrich" };                                            //8 модели бренд покрышек

            string[] descriptionsTrailer = { "2043 EA", "2793 EA", "7209 ГБ", "6782 ЕА", "5148  ЕА", "2129 АХ", "7688 ЕА", "1311 ГСР", "3976 ВЕ", "0675 АК" };    //прицепы 10
            string[] descriptionsTypeAuto = {"Внутри республиканский грузовой", "Служебный (специальный) легковой", "Внутри республиканский автобус",
                                              "Грузовые бортовые","Грузовые самосвалы","Грузовые фургоны","Специальные легковые","Специальные поливомоечные",
                                               "Специальные ассенизационные" ,"Электропогрузчики"};                                                        // типы авто 10

            for (int i = 0; i < driverNumber; i++)  // водители +
            {

                context.Drivers.Add(new Driver
                {
                    FirsLastMidName = getRandomString(customersFirsLastMidName, random).ToString(),
                    Category = Convert.ToInt32(random.Next(1, 5)),
                    RightsNumber = getRandomString(rightNumbers, random).ToString(),
                });
            }
            context.SaveChanges();

            for (int i = 0; i < listTireNumber; i++)          //покрышки +
            {
                context.Tires.Add(new Tire
                {
                    Name = getRandomString(descriptionsTires, random).ToString(),
                    Brand = getRandomString(nameTires, random).ToString(),
                    Date = DateTime.Parse(deviceData[i]),
                    RunStart = Convert.ToInt32(random.Next(0, 150))
                });
            }
            context.SaveChanges();

            for (int i = 0; i < trailerNumber; i++)         //прицепы +
            {
                context.Trailers.Add(new Trailer
                {
                    Number = descriptionsTrailer[i],
                    Massa = Convert.ToInt32(random.Next(1, 10)),
                    TireId = Convert.ToInt32(random.Next(1, listTireNumber))
                });
            }
            context.SaveChanges();

            for (int i = 0; i < positionAuto; i++)         //типы атомобилей  +
            {
                context.TypeAutos.Add(new TypeAuto
                {
                    NameType = descriptionsTypeAuto[i]
                });
            }
            context.SaveChanges();

            for (int i = 0; i < typeOfworkNumber; i++)      //Норма топлива +
            {
                context.NormaFuels.Add(new NormaFuel
                {
                    //Linear = Convert.ToInt32(random.Next(6, 9)),
                    //Summer = Convert.ToInt32(random.Next(5, 7)),
                   // Winter = Convert.ToInt32(random.Next(7, 11))
                    Linear = Math.Round(random.NextDouble() * random.Next(6, 9), 2),  //десятые доли
                    Summer = Math.Round(random.NextDouble() * random.Next(5, 7), 2),
                    Winter = Math.Round(random.NextDouble() * random.Next(7, 11), 2)
                }) ;
            }
            context.SaveChanges();


            for (int i = 0; i < typeFuel; i++)             //вид топлива +
            {
                context.TypeFuels.Add(new TypeFuel
                {
                    Fuel = nameFuel[i],
                    //Cost = Convert.ToInt32(random.Next(1, 3), //!
                    Cost = Math.Round(random.NextDouble() * 2, 2),
                    ToDate = DateTime.Parse(deviceData[i])
                });
            }
            context.SaveChanges();

            for (int i = 0; i < customersNumber; i++)     //зимнее время +
            {
                context.WinterTimes.Add(new WinterTime
                {
                    WinterNorma = Convert.ToInt32(random.Next(1, customersNumber)),
                    DateStart = DateTime.Parse(deviceData[i]),
                    DateEnd = DateTime.Parse(deviceData[i]),
                    Temperature = Math.Round(random.NextDouble() * 40 - 20, 2)
                });
            }
            context.SaveChanges();

            for (int i = 0; i < deviceNumber; i++)         //устройства +
            {
                bool mle = (random.Next(0, 2) == 1 ? true : false);
                context.Devices.Add(new Device
                {
                    Namedevice = getRandomString(nameDevice, random).ToString(),
                    TypeFuelId = Convert.ToInt32(random.Next(1, typeFuel)),
                    SumerTime = DateTime.Parse(deviceData[i]),
                    WinterTimeId = Convert.ToInt32(random.Next(1, deviceNumber)),
                    Harmfulness = mle,
                    TireId = Convert.ToInt32(random.Next(1, listTireNumber))
                });
            }
            context.SaveChanges();

            for (int i = 0; i < customersNumber; i++)                 //машины +
            {
                bool mle = (random.Next(0, 2) == 1 ? true : false);
                bool fle = (random.Next(0, 2) == 1 ? true : false);
                context.AutoCars.Add(new AutoCar
                {
                    NameAuto = getRandomString(customersNameAuto, random).ToString(),
                    Number = getRandomString(customersNumberAuto, random).ToString(),
                    Mileage = Convert.ToInt32(random.Next(100, 1000000)),
                    TypeFuelId = Convert.ToInt32(random.Next(1, typeFuel)),
                    NormaFuelId = Convert.ToInt32(random.Next(1, typeOfworkNumber)),
                    TrailerId = Convert.ToInt32(random.Next(1, trailerNumber)),
                    DriverId = Convert.ToInt32(random.Next(1, customersNumber)),
                    TypeAutoId = Convert.ToInt32(random.Next(1, positionAuto)),
                    TireId = Convert.ToInt32(random.Next(1, listTireNumber)),
                    Harmfulness = Convert.ToInt32(random.Next(1, 255)),
                    Navigation = mle,
                    Injector = fle
                });
            }
            context.SaveChanges();

            for (int i = 0; i < customersNumber; i++)         //Техобслуживание +
            {
                context.Maintenances.Add(new Maintenance
                {
                    TypeTO = getRandomString(customersTO, random).ToString(),
                    AutoCarId = Convert.ToInt32(random.Next(1, customersNumber)),
                    DateTO = DateTime.Parse(deviceData[i])
                });
            }
            context.SaveChanges();

            for (int i = 0; i < customersNumber; i++)        //остаток  +
            {
                context.Balances.Add(new Balance
                {
                    Date = DateTime.Parse(deviceData[i]),
                    AutoCarId = Convert.ToInt32(random.Next(1, customersNumber)),
                    Leftovers = Convert.ToInt32(random.Next(0, 100)),
                    Sug = Convert.ToInt32(random.Next(0, 100))
                });
            }
            context.SaveChanges();

        }

        private static string getRandomString(string[] array, Random random)
        {
            return array[random.Next(0, array.Length)];
        }

    }
}
