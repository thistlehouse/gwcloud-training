using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyStoreApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalToPay = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("020a8c9b-ba57-47b9-8901-f2bd12196722"), "Edison Swaniawski" },
                    { new Guid("033e71c2-23c4-42fe-9173-0d3587e2becb"), "Antonette Jones" },
                    { new Guid("0584cdbf-21e2-4411-b74f-d68cafd1c726"), "Kamryn Yost" },
                    { new Guid("0743961d-c1d2-456a-b18d-6ac695029baf"), "Danny Zieme" },
                    { new Guid("0d8738b3-dc13-4293-a3c7-d85f61fe18dd"), "Tressie Considine" },
                    { new Guid("0ec3dd79-8257-4dff-988b-c2d295034155"), "Jo Considine" },
                    { new Guid("10dd5cf8-5ab5-4521-aaca-ab04aca954ef"), "Pascale Schroeder" },
                    { new Guid("12b5c4c7-cba2-4cc7-af95-e972ffa4098f"), "Hattie Collier" },
                    { new Guid("14e743e5-a39b-43b1-a058-364d5237c1b0"), "Coleman Sporer" },
                    { new Guid("165864de-761c-449d-9e28-9bd02680e154"), "Brandi Yundt" },
                    { new Guid("16692fc1-acd0-4c84-9360-f3c628d10e62"), "Victor Padberg" },
                    { new Guid("169c0e61-199f-4f65-9475-84dcfb171e18"), "Christine Stracke" },
                    { new Guid("1ef72c18-7ff2-4881-a937-ec2c77e8de10"), "Alvah Gerlach" },
                    { new Guid("259e4e40-3043-408f-adc6-aab785db6ba4"), "Moriah Morissette" },
                    { new Guid("284984af-d9fe-4216-be60-0043c1635a55"), "Wilson Hickle" },
                    { new Guid("2adb78a3-ba6d-46c4-853b-b1e0e4eec1d1"), "Elta Sporer" },
                    { new Guid("2f4271d3-6fe7-48cb-bd72-8b88132c1646"), "Morton Harris" },
                    { new Guid("2f820c39-eb4d-48ec-9c69-d9ab81809c82"), "Treva King" },
                    { new Guid("326138bf-39a1-465a-a4a1-111b408b398c"), "Darien Ankunding" },
                    { new Guid("35449e11-6018-4c0a-8840-3bfc68a7f9e7"), "Adrain Spencer" },
                    { new Guid("3603efc7-249a-42ad-a89c-83096f70bb4d"), "Judy Beahan" },
                    { new Guid("367efad4-2dcd-4eec-8c2d-50411f5c9536"), "Maegan Bins" },
                    { new Guid("3a1aee04-6a2a-473f-aa43-3f55d53bb13e"), "Justen Botsford" },
                    { new Guid("3bf38653-a7e0-496d-9e21-62f13818b03e"), "Brannon Wyman" },
                    { new Guid("3d02fdef-7f55-4e53-8e1a-16a580429df0"), "Christy Morar" },
                    { new Guid("417cbd3b-0a66-4653-a743-af4b103460d8"), "Gabe Ziemann" },
                    { new Guid("4325c3c7-61dc-4484-9f13-8799185c2628"), "Bettie Mayert" },
                    { new Guid("4465be3a-57f7-497e-8d65-d8ff1d4618f7"), "Noble Shields" },
                    { new Guid("48ddc1e5-5beb-4bd0-a2a0-576c19c0a3b8"), "Sincere Schinner" },
                    { new Guid("4a62d8c2-d326-49b7-842c-2cf01a8471cb"), "Jaron Grady" },
                    { new Guid("4d9bb0eb-dd2e-46d0-acb5-7e67315f9389"), "Reuben Bode" },
                    { new Guid("521129c3-bb04-4a3a-93f2-1400c15317d6"), "Nikolas Mraz" },
                    { new Guid("523b107d-a14c-44a9-8cde-0962cb345219"), "Stephen Feest" },
                    { new Guid("52be550b-f529-43bd-99e3-402a2f2cc81f"), "Leonardo Dickinson" },
                    { new Guid("53c8a9f6-2126-4369-a2f4-b708d64e4613"), "Erick Herman" },
                    { new Guid("569a09ab-9a05-4431-bb27-271cb34913d4"), "Berta Hermann" },
                    { new Guid("58fa6166-eb89-45b5-9f24-a00a7ea73dde"), "Jeffry Stiedemann" },
                    { new Guid("5b12cfa8-de78-4f04-a88c-6ccdfa9a3423"), "Thalia Cummings" },
                    { new Guid("5f3532f4-3b1f-46e9-b458-d99575d4cd09"), "Ubaldo Stoltenberg" },
                    { new Guid("648d3aa7-70b3-4f56-a5d3-303d96cbb118"), "Kennedi Runolfsson" },
                    { new Guid("64f309de-0e1d-4ef1-bb32-1b05d070e963"), "Marley Collier" },
                    { new Guid("6501fec6-5850-49fa-ab5a-51704f5e9ad5"), "Efren Bashirian" },
                    { new Guid("68e82b90-7d7c-4bb8-93eb-4276a16ef9df"), "Amina Walker" },
                    { new Guid("6cd01a5c-d278-4b87-b104-fc6ab1a164f3"), "Joanie Kertzmann" },
                    { new Guid("6d98231d-72f3-4237-ad70-9383d6d9c9c4"), "Alf Herzog" },
                    { new Guid("6e27899e-ea10-4a32-b046-4d35dcef9ccc"), "Conrad Will" },
                    { new Guid("716ff577-7e31-417d-9ea3-c08af2d1c669"), "Concepcion Harvey" },
                    { new Guid("7764aa49-7f7f-4d80-9617-1b51bda68d6c"), "Myron Altenwerth" },
                    { new Guid("77aeb387-b0bb-47df-9ffb-c4303f80c920"), "Samson Torp" },
                    { new Guid("77afc0cc-cf25-42eb-9e75-6cebf223f633"), "Ashton Cronin" },
                    { new Guid("7a697a6c-edaa-46f2-9621-50636faa310e"), "Lorena Zemlak" },
                    { new Guid("7bec02d4-17cc-4d99-adf4-e855e4eb0e47"), "Mack Glover" },
                    { new Guid("7c1e4347-500f-4a38-82d4-a9d9053085df"), "Gene Harber" },
                    { new Guid("7d15ebad-377f-4f9a-a103-74b9172a4b95"), "Ricky Abernathy" },
                    { new Guid("7df4dc5b-9c4e-4d35-87c6-068ca735a414"), "Savanah Schultz" },
                    { new Guid("7ffc770c-bebe-474c-83c3-4efb4d400577"), "Arch Watsica" },
                    { new Guid("824f2dba-91a4-404b-b59b-ccc85327b54a"), "Leon Schiller" },
                    { new Guid("8282e38a-5707-484e-ab1e-8dbf205b8947"), "Neha Anderson" },
                    { new Guid("82cb5406-fa58-442e-86de-46bdb0f1344c"), "Ocie Gerhold" },
                    { new Guid("878415c6-7a76-44e1-ba1e-eaaf86f767c4"), "Elijah Pouros" },
                    { new Guid("89a6f180-affa-4e9d-a196-355a61f2884b"), "Wilson Schowalter" },
                    { new Guid("8d92f6f3-8b7e-4b36-a040-fc7dba5f891d"), "Abdul Jacobson" },
                    { new Guid("8dd009a1-0888-48d8-9e75-f28ff9c6e834"), "Leopold Hansen" },
                    { new Guid("8ea23057-8289-4de5-ae77-cf2e6b62de37"), "Joshua Rolfson" },
                    { new Guid("8edb1492-31c3-4fcd-8d33-7c1aa4552ad1"), "Tatyana Sipes" },
                    { new Guid("92c8d2b1-fea6-4222-96e6-c6fb44eb7649"), "Marisol Gutmann" },
                    { new Guid("9da2ab17-8bea-48c5-a83c-13483dfd67e4"), "Patience Kling" },
                    { new Guid("a1236bcc-eda4-4e28-8807-59f6c11d521f"), "Deion Pfannerstill" },
                    { new Guid("a129e678-169f-4d7c-bbc8-a3bceac77bad"), "Jarrett Rowe" },
                    { new Guid("a2dd57cb-7523-447f-95c6-80f1fc27bfc4"), "Torey Waters" },
                    { new Guid("a8a40d37-3e46-4117-b85d-402f4442e2de"), "Kamren Dare" },
                    { new Guid("a8fb08e8-b3c9-464e-a919-b18ed6674c81"), "Violette Lemke" },
                    { new Guid("ace61927-503f-4664-b346-3d4d93972b0f"), "Gina Ferry" },
                    { new Guid("acef7e78-8988-4b79-9e44-c945368129a9"), "Abdiel Stanton" },
                    { new Guid("ad958b00-cfb3-4da5-abac-56977ea2152c"), "Mellie Bergstrom" },
                    { new Guid("ae33c188-0481-4356-b919-305135ce46ed"), "Shawna Zemlak" },
                    { new Guid("ae439c8d-a1c6-4747-9889-c0b0dbaec25b"), "Clark Bartoletti" },
                    { new Guid("b3413d0b-1d8b-4a3a-915e-a42499f87e8d"), "Maryam Kunze" },
                    { new Guid("b5007658-f86e-4d12-91f1-874e52f7e6cf"), "Geovany Collier" },
                    { new Guid("be51d1ba-a131-4b60-b2e8-8cab8161b103"), "Selmer Wunsch" },
                    { new Guid("c603369d-4fdd-4aaf-825a-921956b3700d"), "Shannon Will" },
                    { new Guid("c69ef869-2910-4090-9f02-40a71807b97f"), "Karli McClure" },
                    { new Guid("c6c66eac-9f81-487f-922c-2acd73f5f1fe"), "Antone Steuber" },
                    { new Guid("c8ea9ce0-80ab-4928-a5cf-4583071ee21c"), "London Jakubowski" },
                    { new Guid("ca378142-1bfe-497b-a817-717c88db70a9"), "Lemuel Kertzmann" },
                    { new Guid("d0ae71a3-c306-4620-870d-4808f4043ab9"), "Kamryn Bins" },
                    { new Guid("d926d9d8-d2d5-43ff-bc86-614b103c2dbe"), "Baron Auer" },
                    { new Guid("dbfa7b4f-b409-41ba-a236-badd056465c9"), "Allen Becker" },
                    { new Guid("df423dfe-3412-4f98-b3f0-88848a4ec1c7"), "Chyna Prosacco" },
                    { new Guid("e06a8a8a-c675-43ff-9cc7-f1d702967fe6"), "Clint Nolan" },
                    { new Guid("e86fb9d1-b180-40b5-88ab-81264f900d66"), "Lynn Bradtke" },
                    { new Guid("ea69184e-7f96-4aa6-8fc9-eda626ea957a"), "Elbert Hauck" },
                    { new Guid("f29126d2-ea31-415d-8ea9-4aa016411ef5"), "Deion Nolan" },
                    { new Guid("f4cb2361-2e3e-4db2-afc5-f70f99380638"), "Grover Volkman" },
                    { new Guid("f5a37294-b438-461c-965c-28dfa6275e78"), "Jalyn Wisozk" },
                    { new Guid("f9c154d3-3279-4017-980f-bde964bfe834"), "Nick Gibson" },
                    { new Guid("fa2016ff-d334-48da-82b9-eed55382f742"), "Hanna Hilll" },
                    { new Guid("fab71618-db7a-487a-b30b-a11688eeffa7"), "Selina Little" },
                    { new Guid("fc26c4de-cc94-479a-9509-b577a255cc00"), "Eloise Blick" },
                    { new Guid("fc342970-7b12-421d-99ec-bb238cacf04c"), "Mohammed Stracke" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("01d36197-f384-42c4-85ac-2ccad677fea4"), "Intelligent Wooden Computer", 155.78m },
                    { new Guid("02853b1e-d5b5-4c42-88e6-8db826d9157b"), "Tasty Granite Ball", 55.65m },
                    { new Guid("033ffa09-3c96-4f23-ad70-1b32b08f8032"), "Refined Fresh Bike", 274.04m },
                    { new Guid("05a2cf4c-cef1-4266-a60b-a55fd5a970ba"), "Sleek Rubber Tuna", 176.84m },
                    { new Guid("0eb7f89a-cef3-4b76-9e03-d6121604e2f7"), "Small Soft Pizza", 177.55m },
                    { new Guid("0fbc9c25-04fb-4cac-9e59-44d146bcbbde"), "Incredible Rubber Pants", 66.34m },
                    { new Guid("109fda6c-6c31-478e-96d2-e846a3df1df9"), "Handmade Fresh Ball", 55.14m },
                    { new Guid("140dc1c2-573e-4366-83f8-b0e07d85714a"), "Intelligent Concrete Pizza", 114.67m },
                    { new Guid("143cc0ca-98fb-4cc1-8837-3ade59cf4f73"), "Handcrafted Fresh Car", 251.93m },
                    { new Guid("17ae9119-e14c-477f-9eaa-14f7ca720046"), "Rustic Frozen Sausages", 243.71m },
                    { new Guid("180f20bf-ade6-489e-8e90-5b893c0cb869"), "Incredible Metal Computer", 270.52m },
                    { new Guid("18afdfe7-cf9a-4a5a-a272-9781d9483dc5"), "Generic Metal Pizza", 194.55m },
                    { new Guid("2000f74b-3cc2-4ac0-b044-9029def0fda5"), "Refined Wooden Shoes", 199.40m },
                    { new Guid("2069c5bc-4c7a-4852-b118-9b542fad298a"), "Handcrafted Fresh Hat", 75.91m },
                    { new Guid("29d6a8f2-0bb0-40f9-96f9-fd489dd65e5d"), "Fantastic Steel Soap", 24.69m },
                    { new Guid("2de1569c-5b90-4b15-aba5-3be6e9dd015e"), "Practical Metal Mouse", 261.71m },
                    { new Guid("2e12306e-de0e-47e0-a101-8cf284252116"), "Sleek Rubber Cheese", 238.85m },
                    { new Guid("2e323154-cd6c-4ed3-9e66-d45ef49e1d24"), "Incredible Cotton Bike", 178.63m },
                    { new Guid("301f47c7-a8e2-43f9-b6fa-e6c6ecd733a4"), "Incredible Steel Car", 32.70m },
                    { new Guid("33aef15b-df70-4df9-8fc7-da1776219395"), "Awesome Wooden Hat", 291.80m },
                    { new Guid("3506b191-678b-4aaf-94ca-f2fb722e465c"), "Generic Steel Chips", 36.41m },
                    { new Guid("3a09f113-043e-430c-af77-4e1ba969efd7"), "Practical Steel Chair", 150.58m },
                    { new Guid("3ace9af1-1994-4c2b-b008-0e44158afed2"), "Refined Granite Mouse", 240.74m },
                    { new Guid("3cefc224-4bcc-4039-9a5e-cdd3ac2394b4"), "Awesome Steel Ball", 153.81m },
                    { new Guid("3f3b99a8-aa1b-4ebd-9f8a-c7615e7800a6"), "Incredible Wooden Shoes", 253.30m },
                    { new Guid("3fb5862f-1750-4e63-b15b-3ad2c8e77063"), "Sleek Rubber Table", 39.35m },
                    { new Guid("40e0ffb2-55c2-4810-994a-ec611968e6d6"), "Handcrafted Plastic Fish", 142.15m },
                    { new Guid("41ae1f77-815d-4b16-afa5-eb4866ff4dfa"), "Gorgeous Metal Table", 182.09m },
                    { new Guid("42275561-e1f8-401a-8eda-d1bbdb32698d"), "Handmade Rubber Keyboard", 259.87m },
                    { new Guid("45d71d4a-dda1-40e5-8faf-db3fef480412"), "Handcrafted Soft Shirt", 179.31m },
                    { new Guid("4696cd45-e8d2-49aa-8a26-46b4ecc5aadd"), "Incredible Soft Shoes", 166.94m },
                    { new Guid("49cc5be6-2815-482f-9ff8-0e226681d534"), "Handcrafted Soft Chair", 18.57m },
                    { new Guid("4a98ad4c-1f0e-4727-89a9-7b233ae22458"), "Rustic Plastic Bacon", 159.12m },
                    { new Guid("4e69a56e-349b-4614-ba74-b9df50d07021"), "Incredible Steel Chips", 77.59m },
                    { new Guid("50b7b8ad-810c-48c0-b78d-9714b92b967d"), "Gorgeous Rubber Salad", 70.45m },
                    { new Guid("50d0c902-a8b5-4e88-a2fd-4e43b85d85fa"), "Tasty Fresh Chips", 282.10m },
                    { new Guid("50fa234c-b866-468f-8226-dec2f5a2e925"), "Handmade Wooden Shirt", 239.47m },
                    { new Guid("54282ad3-2145-4e1d-a4ac-5a11e18a82b2"), "Awesome Rubber Mouse", 154.05m },
                    { new Guid("55e15224-1721-460d-9dad-2f88ae9c0b24"), "Awesome Wooden Shoes", 227.53m },
                    { new Guid("58ac2d0a-7aff-4c32-ba1a-6dc9c0c7a948"), "Handcrafted Rubber Car", 33.16m },
                    { new Guid("5f264ece-c028-4812-aad6-0a107703cab7"), "Generic Metal Towels", 219.75m },
                    { new Guid("6579bc25-934f-4b25-a98b-87859d82992f"), "Generic Soft Mouse", 245.53m },
                    { new Guid("6a44c346-c5ea-4d20-af68-61e4f192b744"), "Gorgeous Soft Cheese", 52.05m },
                    { new Guid("6b3ae872-29e3-441b-a3fc-2ebaec57241b"), "Awesome Plastic Pizza", 89.91m },
                    { new Guid("6b50247e-0329-4e15-9b10-9eeb2c5d09f9"), "Generic Soft Chicken", 129.49m },
                    { new Guid("6bbde8d0-36af-4022-b3f8-039be875563f"), "Fantastic Metal Chair", 103.84m },
                    { new Guid("6bea02ad-69f0-4ad3-9819-56b9735c69b1"), "Sleek Plastic Mouse", 275.90m },
                    { new Guid("7012c6f8-1f75-43c8-80ca-b107ce223c71"), "Handmade Cotton Soap", 8.56m },
                    { new Guid("712a592e-36b8-4956-a7c5-5e76ada44746"), "Incredible Fresh Salad", 84.06m },
                    { new Guid("717e9c9a-b220-44ce-8089-43832d5e3584"), "Licensed Granite Salad", 31.15m },
                    { new Guid("71af168e-a979-4e5d-ba5a-0e0e5b0945b5"), "Rustic Rubber Sausages", 226.89m },
                    { new Guid("76f16cb1-5bf9-40a2-9db4-84de9747df2c"), "Incredible Plastic Bike", 14.87m },
                    { new Guid("78f13036-9a9a-4da5-912e-463f9d4ac525"), "Gorgeous Steel Ball", 274.36m },
                    { new Guid("7c497b48-156c-4aec-b3e9-9f27b72e5877"), "Gorgeous Granite Shirt", 59.49m },
                    { new Guid("7eeaa754-f301-42bb-8564-68d4a6272d86"), "Licensed Fresh Mouse", 91.19m },
                    { new Guid("81e9677e-a218-42c7-a678-2555e356559f"), "Ergonomic Rubber Towels", 194.87m },
                    { new Guid("81f9a537-68e8-47fe-bac8-fa8e9607a520"), "Handmade Rubber Keyboard", 29.36m },
                    { new Guid("84c0736e-5d2e-4b5d-afce-f0b09b860821"), "Fantastic Fresh Keyboard", 29.14m },
                    { new Guid("88a9f6b5-b97c-4509-b88f-8c71c64d1368"), "Practical Cotton Shoes", 154.87m },
                    { new Guid("93b8573e-ba43-48c6-93ed-7c4c536238f4"), "Sleek Rubber Sausages", 24.87m },
                    { new Guid("9807c6a1-cede-433b-bd1d-73b24563f9ec"), "Generic Wooden Table", 105.24m },
                    { new Guid("982b7487-fddc-4be5-98ec-b0ffc41368eb"), "Intelligent Concrete Tuna", 197.79m },
                    { new Guid("99fcbfae-4834-463f-98fc-a3ae8ac7f359"), "Sleek Rubber Shoes", 299.58m },
                    { new Guid("a0bcc82a-7343-45ed-bdd9-a06cb261d0b5"), "Tasty Steel Cheese", 262.94m },
                    { new Guid("a7a9a87c-3c3c-4726-92f7-f69e7f568eb4"), "Sleek Metal Fish", 276.57m },
                    { new Guid("b0a35b4b-9446-43ec-b71d-7e4e62474f2b"), "Practical Metal Cheese", 176.50m },
                    { new Guid("b448bdaa-d044-4d1d-8a5e-aefc710563f2"), "Tasty Metal Fish", 9.04m },
                    { new Guid("b8bda5c6-23cd-454e-97c5-a574024c2caa"), "Incredible Cotton Bacon", 157.12m },
                    { new Guid("bba6209a-a9b7-4c6c-b1f6-832082858d7c"), "Unbranded Fresh Fish", 17.25m },
                    { new Guid("bea00e11-e722-4886-95cf-d7cb125e5129"), "Unbranded Steel Tuna", 137.76m },
                    { new Guid("beb3cbe9-df4b-455d-9153-1b70db47bed8"), "Rustic Metal Soap", 243.40m },
                    { new Guid("c2e03372-41a7-47b9-87a3-a176b77948c3"), "Ergonomic Soft Towels", 283.47m },
                    { new Guid("c52f6318-2530-42fc-a2e2-274a2f2cad5a"), "Small Concrete Soap", 198.43m },
                    { new Guid("ca55a91b-8971-414c-b343-e7d249c97ac4"), "Small Plastic Towels", 259.53m },
                    { new Guid("cabd8592-58b5-4588-8982-525b2eb6fc7d"), "Unbranded Plastic Bike", 195.85m },
                    { new Guid("cc31130c-a843-4552-a04a-5504e75ec000"), "Unbranded Soft Chair", 73.47m },
                    { new Guid("cd66b509-e1df-4de9-84ad-d865fb47f225"), "Sleek Wooden Chair", 41.95m },
                    { new Guid("cff75f9e-c6f4-48d9-94f0-195b13dd08c3"), "Intelligent Metal Tuna", 188.75m },
                    { new Guid("d25aa876-164d-4153-a340-1d34e87fe2b6"), "Incredible Frozen Tuna", 92.90m },
                    { new Guid("d3622749-cd90-4808-a536-92d146f7749f"), "Licensed Wooden Chair", 158.49m },
                    { new Guid("d41d8175-3c84-48cb-a27e-1ccb37e8c09c"), "Unbranded Plastic Pants", 237.52m },
                    { new Guid("d436f580-3426-4355-9bec-3989e1817f85"), "Refined Soft Hat", 184.00m },
                    { new Guid("d6d83090-080d-4de3-9f7f-835c80d47097"), "Intelligent Wooden Fish", 138.03m },
                    { new Guid("e2d77964-d14b-47d6-8857-4cc110adaaf5"), "Ergonomic Cotton Keyboard", 275.06m },
                    { new Guid("e49fde84-3abe-4e8b-a53c-50dc93533d8f"), "Intelligent Plastic Bike", 40.82m },
                    { new Guid("e58b5aba-d582-4ff4-b01c-47fcd977b437"), "Refined Rubber Gloves", 69.00m },
                    { new Guid("e7172b2a-3b56-45ca-a4e9-98dd4b1e4fcb"), "Practical Fresh Hat", 43.35m },
                    { new Guid("e74525f0-d0d7-4fd5-b157-10e6e69b4ca3"), "Intelligent Rubber Chair", 190.80m },
                    { new Guid("eb6dece6-8fe2-4e06-9ded-52ad733cd383"), "Unbranded Steel Chips", 14.51m },
                    { new Guid("ec31e3d5-3c5a-45aa-a9d5-184281015eab"), "Sleek Granite Bike", 85.50m },
                    { new Guid("edeab8f4-481f-450b-a52f-6ee6842b5acd"), "Gorgeous Granite Mouse", 11.49m },
                    { new Guid("f279add2-407a-45c3-95f7-0e798e93b4e8"), "Handmade Granite Computer", 90.41m },
                    { new Guid("f2e64988-33fa-4857-8018-fb9e06776727"), "Practical Plastic Hat", 291.32m },
                    { new Guid("f424952c-ae44-44c0-aa4e-a9e2b01e8b3f"), "Small Concrete Bike", 258.56m },
                    { new Guid("f483b45a-73c8-4a6a-b0e1-872319abe6ca"), "Licensed Soft Hat", 132.25m },
                    { new Guid("f63bed31-c2b5-4376-b286-ba598d652647"), "Sleek Steel Gloves", 118.41m },
                    { new Guid("fb8b1d92-1b11-4d2f-a212-f465883432a7"), "Refined Metal Pants", 278.39m },
                    { new Guid("fc1669b0-bd7b-4564-9d0f-efcdaaa6ffbf"), "Licensed Wooden Mouse", 147.85m },
                    { new Guid("fc9b0623-c63d-4992-b33d-a040a400cc6b"), "Fantastic Concrete Hat", 194.50m },
                    { new Guid("fd56e4c1-1a47-42a3-9894-a2a511c4569e"), "Refined Cotton Fish", 55.76m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_ProductId",
                table: "OrdersProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
