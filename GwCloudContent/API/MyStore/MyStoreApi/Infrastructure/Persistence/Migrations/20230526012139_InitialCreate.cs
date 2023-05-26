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
                    TotalToPay = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: true)
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
                    { new Guid("011264e2-3a66-4d58-a68c-2aa8ffdc0fc5"), "Marta Becker" },
                    { new Guid("01642da5-23f9-48af-96ea-59f29caf1587"), "Alfonso Baumbach" },
                    { new Guid("05bce788-2d6d-476f-9587-db96f85c7b82"), "Helen Reilly" },
                    { new Guid("0b1ab7a9-7cae-40fb-a28e-c1b0b8b108bd"), "Octavia Purdy" },
                    { new Guid("0c092d20-6b58-4c9a-bac9-39f12082b465"), "Brandy Shanahan" },
                    { new Guid("0c1f8a62-c7d7-4c1f-af29-281e224ebee7"), "Gretchen Lindgren" },
                    { new Guid("0d45b609-551a-4940-a8b5-527b748163d4"), "Mohammad Effertz" },
                    { new Guid("0eed1cd7-e9cb-449a-97e2-234ab368b012"), "Ines Bechtelar" },
                    { new Guid("10bfa04d-e9e2-4163-b6f2-139f9ae254cb"), "Natasha Halvorson" },
                    { new Guid("12f41188-2c8a-48c4-a648-4440f781b32e"), "Kaci Ferry" },
                    { new Guid("130c64a8-9ead-4ca8-8ece-5a14c119a525"), "Shayna Ryan" },
                    { new Guid("13a22cd4-dcc9-4d03-8515-dee0bc6960b3"), "Orie Hintz" },
                    { new Guid("1557fc71-17d2-4f36-88b6-96944f9f1df5"), "Jakayla Hammes" },
                    { new Guid("179e27c2-03da-4771-9891-984c097455db"), "Mollie Hermann" },
                    { new Guid("1a92014e-4bbb-4749-92c2-d92d6e79698b"), "Frankie Ledner" },
                    { new Guid("1d1ff7b6-1d3b-47a0-802d-c55d3ac78699"), "Olen Sipes" },
                    { new Guid("1e96d458-64b1-4a8f-80ee-580b71a231ca"), "Easter Morissette" },
                    { new Guid("1f76eae9-f910-4d07-b75b-5856ca243049"), "Stacey Spinka" },
                    { new Guid("27187fc8-eebe-448a-9c5d-22af8c6706d4"), "Lilyan O'Conner" },
                    { new Guid("276e910e-1565-4056-95ba-60046b18cc5d"), "Katharina Kling" },
                    { new Guid("2825c6ff-07f0-48ea-8867-2024b2c9531e"), "Maud Lemke" },
                    { new Guid("2997973b-43cc-45c0-a8ec-94eb094bcf45"), "Morris Prosacco" },
                    { new Guid("2cfe3a8e-b0a6-4b5f-8f3a-9d84ea584526"), "Myron Nader" },
                    { new Guid("2eea49c8-6224-4d2e-ac70-569a254af5d0"), "Tod Leannon" },
                    { new Guid("31cab3de-1dd3-4588-9db8-138263020556"), "Antonietta Pagac" },
                    { new Guid("31e4fc94-3831-4d65-bbc8-2e0462ddb4c3"), "Marlin Ward" },
                    { new Guid("3373ee76-b159-4969-8fcd-5f1f2af33ae4"), "Caesar Murazik" },
                    { new Guid("35836010-475d-470a-ad11-b7a8bc81497d"), "General Corkery" },
                    { new Guid("3673db53-7811-4154-bd2f-0ce73bc4e4b2"), "Isac Shields" },
                    { new Guid("3819aaea-f2f4-46b7-8a57-e83bff0e3cad"), "Marisa Wilderman" },
                    { new Guid("3bee1944-1890-4571-94a0-bd10a13981f1"), "Kirsten Koepp" },
                    { new Guid("3e2bf2eb-9dc6-4af2-952f-b297b81471ed"), "Adolphus Yost" },
                    { new Guid("3f05bbc6-bb62-4b61-8645-1617bfe44193"), "Tyreek Corwin" },
                    { new Guid("40ec58e6-70a9-4a1d-ba58-2aee95a126b2"), "Wilhelmine Mayer" },
                    { new Guid("410ddc95-57f0-4d0e-b03b-d801ef07461c"), "Karianne Ondricka" },
                    { new Guid("42b3e3c3-8bc2-4bcc-818d-1e6ef1585151"), "Viola Greenfelder" },
                    { new Guid("48a00891-3010-48c6-984b-25c25447dcbe"), "Marian Gutmann" },
                    { new Guid("48e9ff5f-2417-4bdf-84dc-cc8accdb7ae4"), "Jed Witting" },
                    { new Guid("4ab0ecaa-2e57-4c80-b2ec-d05bf70cbbc4"), "Elinor Kuhlman" },
                    { new Guid("4b393eb5-f251-4037-8c85-5eaefce37b85"), "Brennon Cartwright" },
                    { new Guid("50979086-36a2-49b3-abe1-d84651b58b5d"), "Hilda Borer" },
                    { new Guid("546d016a-ce60-46b4-aa50-334506caeddc"), "Angelo Larkin" },
                    { new Guid("598be7b8-7b0e-44b6-bcf0-82783e28920c"), "Dandre Ratke" },
                    { new Guid("5c466f32-f92b-4331-b835-19de9dcc39df"), "Jarrell Streich" },
                    { new Guid("5c71787a-c460-4129-b49f-ff25e908b41c"), "Morgan Kuvalis" },
                    { new Guid("5d1c8843-82e7-4c32-9feb-f5bdabda9e57"), "Asha Langworth" },
                    { new Guid("61eaa6e9-7412-4972-9686-6adc776a74be"), "Benedict Gorczany" },
                    { new Guid("63d3d95a-b80d-442b-af95-6ed424b26b49"), "Ned Heller" },
                    { new Guid("66656bce-182e-4583-8c15-6eb11c9c819a"), "Jeromy Schoen" },
                    { new Guid("6909816c-e481-45e1-9026-8dfdb2710bb1"), "Antonia Kshlerin" },
                    { new Guid("6977bbbd-d63b-401e-96a3-f99cc9dc2d54"), "Elmira Spencer" },
                    { new Guid("6d0e766c-4ebe-4e02-afa9-407d39e72a76"), "Ike Watsica" },
                    { new Guid("742923e8-79fd-4d64-af97-7419486f168a"), "Dejon Beier" },
                    { new Guid("74d21d8b-4573-4060-810d-712275d4301a"), "Dexter Lubowitz" },
                    { new Guid("750666cf-74c3-4d3e-97de-92c026e82481"), "Nicolette Dare" },
                    { new Guid("819eee36-5fe7-4766-a788-c72f2634c85c"), "Richard Brown" },
                    { new Guid("8296d363-c1c5-4ab4-8304-605f50a7f55b"), "Isadore Stroman" },
                    { new Guid("82de1064-06ac-468b-bd41-93ccabc81b01"), "Sebastian Little" },
                    { new Guid("8a2d0dbb-93aa-455b-9baf-070d57fc2e73"), "Marion Marquardt" },
                    { new Guid("8a3cfad3-38d5-4734-a316-9624ddccb4d1"), "Emmanuel Hodkiewicz" },
                    { new Guid("8ab24ca9-87f3-4a0c-9d73-ac939c9a5380"), "Nicholaus Haag" },
                    { new Guid("8bc8c0b8-daf3-4c1b-afaa-9b049feb829c"), "Eunice Predovic" },
                    { new Guid("8f6ddc9d-2c6e-4ef7-954b-744f95750bb7"), "Ubaldo Collier" },
                    { new Guid("9515e3ee-87aa-468f-a981-995dfd4610d0"), "Jacques Konopelski" },
                    { new Guid("95df60cd-cfdc-4463-88ed-9a17fe845cbc"), "Dorothy Wiegand" },
                    { new Guid("96186ddb-fffa-488a-b30a-832a7b8780e6"), "Esther Padberg" },
                    { new Guid("9cc33c3b-b638-4bad-8a95-0fd6d2a1d9e1"), "Nicholaus Reynolds" },
                    { new Guid("a194777b-cd7e-4cb1-b778-08cb609d8cef"), "Paolo Oberbrunner" },
                    { new Guid("a67aa51d-1809-404e-985d-bfd361bc23f1"), "Reuben Grimes" },
                    { new Guid("ab46fde6-fda9-4071-a66c-1afa478464ed"), "Zoie Waters" },
                    { new Guid("aebfe16e-0d63-4463-9742-1e21e73b828d"), "Mikayla Smith" },
                    { new Guid("b325a924-4da3-4433-9864-b7e4b434c7b8"), "Fredy Lind" },
                    { new Guid("b3b08c00-d622-4f2a-99ea-6af00ba2fc96"), "Theodore Jast" },
                    { new Guid("b6fb095b-c26f-4388-a3fc-c7c3231cc7a2"), "Greyson Champlin" },
                    { new Guid("bd5f48f6-d11b-4070-b698-e025216cf7cb"), "Alf Hammes" },
                    { new Guid("bd76261d-7404-4986-83d3-cb3a7b099485"), "Dillan Ward" },
                    { new Guid("bd9b8d19-f547-4ce6-b8b7-ff3f13783ef6"), "Merle Feest" },
                    { new Guid("be75fe6e-9e86-45b7-bb30-9f385cf5ae26"), "Thurman Weber" },
                    { new Guid("c01190a9-698b-4cf4-ad5d-effb4ab1126e"), "Raul Gleichner" },
                    { new Guid("c610da2e-8640-42b5-abd0-69064779d536"), "Victoria Roberts" },
                    { new Guid("c6dd9265-146f-43a0-8bfd-e06b752e41d0"), "Shawna Thiel" },
                    { new Guid("c7761333-e856-497f-8c13-ac2f80bcf6c5"), "Emmett Harvey" },
                    { new Guid("cca29b49-7add-4984-9dc7-59e8f6282255"), "Karina Morissette" },
                    { new Guid("ce7d35d4-4e30-4370-a62c-f8e8afb2dd90"), "Florine Robel" },
                    { new Guid("e19c6184-d5f4-4adf-8464-8f353f3d134d"), "Nestor Thompson" },
                    { new Guid("e82662c0-61ad-4f47-b147-7af9029d67e7"), "Judah Hackett" },
                    { new Guid("e88325eb-7c20-4d54-9ce2-f047d35574e8"), "Krista Wolf" },
                    { new Guid("e8feddd6-7d83-4820-9923-95c721fddbce"), "Gudrun Swaniawski" },
                    { new Guid("e99725e0-4d5a-4af8-815e-d753d6ac21eb"), "Chadd Borer" },
                    { new Guid("eb47f6ed-0da1-4d6c-8c60-42a7a2739ce4"), "Kaylin Doyle" },
                    { new Guid("ecec11e8-746b-49b9-9a41-07dfc522a617"), "Kyla Rohan" },
                    { new Guid("ef4bd976-e47c-4e3e-a42c-40c872ff5054"), "Kip Witting" },
                    { new Guid("f11dbef2-c3b4-4f0c-9107-854e09c048e3"), "Cristobal Friesen" },
                    { new Guid("f488784c-dc98-4830-99e9-9d4c21bd7f8e"), "Krista Bosco" },
                    { new Guid("f5433967-2a63-4e11-bf81-c584ae8d3aff"), "Maverick Denesik" },
                    { new Guid("f651d54e-1d54-4b6c-ac56-d215b2468daf"), "Milo Heathcote" },
                    { new Guid("f9c8599b-f35b-4011-8751-b4dcf180a8ff"), "Casper Erdman" },
                    { new Guid("f9e1e97a-1e65-41db-b798-52f61c8c0fe5"), "Zachary Hickle" },
                    { new Guid("fae63244-73b0-4215-8ee7-208bff789264"), "Albert Dietrich" },
                    { new Guid("fb396bd8-4cd6-4602-887e-6af978cbb2aa"), "Stefan Kerluke" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("00916c15-4174-4953-9966-81d1224b3264"), "Gorgeous Rubber Fish", 80.46m },
                    { new Guid("0114b516-dd48-44ac-a7d4-045286bf80e1"), "Gorgeous Granite Pants", 110.99m },
                    { new Guid("033efaef-fc39-479c-9dcc-a56c3d95869d"), "Small Rubber Car", 292.01m },
                    { new Guid("06ce8ff9-6bf7-42fe-b0ac-37cf01a637b4"), "Refined Fresh Soap", 244.31m },
                    { new Guid("0b42fbbb-b9cc-4dc6-9978-bc24ae05e154"), "Tasty Metal Cheese", 68.76m },
                    { new Guid("0f1dd960-5029-45e4-8ebc-ad4d1ff97a02"), "Incredible Plastic Pizza", 159.67m },
                    { new Guid("11095d55-c390-491f-9fb9-7f590f7b06df"), "Sleek Concrete Sausages", 17.31m },
                    { new Guid("15806122-8acb-47e8-9429-143f980b451b"), "Intelligent Rubber Pizza", 214.08m },
                    { new Guid("15fbbac3-68ca-4778-8641-7d4d8142a13b"), "Awesome Fresh Chicken", 153.48m },
                    { new Guid("1700963c-aa89-4442-ae53-7bd9443913e4"), "Unbranded Wooden Soap", 103.09m },
                    { new Guid("20a1e737-d36d-46c1-87ab-8d7e2e0e4790"), "Refined Soft Chair", 6.54m },
                    { new Guid("21cf91b1-8c50-4747-9176-9ea66b5082a1"), "Small Fresh Bike", 154.26m },
                    { new Guid("2257d785-9523-4b14-aeab-862ed5bc8964"), "Small Fresh Pants", 47.05m },
                    { new Guid("2312e659-cdb0-4aec-9d77-845649dd85f5"), "Tasty Cotton Gloves", 164.37m },
                    { new Guid("2435232a-06fa-46c1-aa6c-56fec5967786"), "Handcrafted Rubber Hat", 156.44m },
                    { new Guid("28140b37-e95b-4f42-bf1e-7b746626f7e1"), "Ergonomic Rubber Soap", 137.93m },
                    { new Guid("2ac26484-305c-4851-b345-df5fcc8a96de"), "Sleek Soft Car", 274.53m },
                    { new Guid("2ad9af24-97fe-4178-8de7-4aacedeaf60c"), "Intelligent Wooden Bike", 176.88m },
                    { new Guid("2bcf22f8-00ca-46d6-9d40-a3bdfb45b895"), "Incredible Concrete Chicken", 278.09m },
                    { new Guid("2d50500e-5f07-466e-96b4-78e76b79dd02"), "Generic Rubber Pants", 182.00m },
                    { new Guid("2e740c88-3a25-4980-9df2-a636ae4a2954"), "Fantastic Metal Bacon", 201.18m },
                    { new Guid("2f47f73a-0a3b-46f9-8202-28b69e7ea5f4"), "Refined Soft Ball", 286.71m },
                    { new Guid("3250d98b-3b2a-4e43-a112-637618b07f50"), "Intelligent Fresh Shoes", 158.06m },
                    { new Guid("32cb9d3f-7983-46d7-bb2b-233c7117dff0"), "Incredible Frozen Shirt", 173.67m },
                    { new Guid("3442d493-1fc5-483e-a43f-e7c828231efb"), "Ergonomic Metal Sausages", 205.93m },
                    { new Guid("3b4d324f-1ac4-4e4e-abf9-d94e60148f72"), "Gorgeous Rubber Bacon", 188.77m },
                    { new Guid("3e6d006d-48a8-4932-b1d5-643abdd9c8d8"), "Unbranded Steel Hat", 266.42m },
                    { new Guid("3fbd0984-1824-4997-92a7-d7456ab5da14"), "Handcrafted Granite Chips", 75.18m },
                    { new Guid("40ab2e49-5beb-4b60-890a-47f9b53cd68f"), "Practical Rubber Fish", 77.50m },
                    { new Guid("41ccc4d3-cd6f-40c6-84c9-7797a711ec70"), "Unbranded Steel Soap", 157.89m },
                    { new Guid("44c4d0a5-22a0-4c98-897f-7e2ce265fc2c"), "Licensed Concrete Shoes", 124.20m },
                    { new Guid("4824d2d3-750e-4d0a-9de5-03d71a8bd7a1"), "Sleek Steel Chicken", 282.09m },
                    { new Guid("4b7b8f3f-4b06-4cb5-a090-504aa0f65088"), "Sleek Granite Chair", 231.90m },
                    { new Guid("4cae62bf-5e6a-4d3e-8f6d-132e57390a43"), "Intelligent Cotton Salad", 192.89m },
                    { new Guid("4dc631db-0098-4ca3-880b-7ba87a47aa8b"), "Intelligent Fresh Soap", 159.22m },
                    { new Guid("4e061440-9178-41e9-934a-07752c46b93a"), "Handmade Rubber Car", 68.24m },
                    { new Guid("4f426222-5b05-4812-aa3d-735b443105ec"), "Licensed Plastic Chips", 164.89m },
                    { new Guid("5102e9bf-3df0-4ef7-856b-3048b2f7d62d"), "Ergonomic Wooden Chips", 162.15m },
                    { new Guid("51886537-f64f-4548-bbc6-9ff556759754"), "Practical Plastic Sausages", 21.67m },
                    { new Guid("561d6d2a-29c3-4259-a35c-4575ab4678e8"), "Gorgeous Cotton Shoes", 104.68m },
                    { new Guid("63bd776b-2372-41f3-a01b-228a638ba207"), "Rustic Rubber Bike", 117.75m },
                    { new Guid("66075a57-ffcc-443c-92e7-85ceb81f88b8"), "Generic Soft Chicken", 66.45m },
                    { new Guid("6a3d0ad0-cdf3-4da3-972e-6ffbb1f77ef0"), "Fantastic Frozen Shoes", 92.73m },
                    { new Guid("6b70fbb0-f498-4bb0-850d-698bfc27dc06"), "Ergonomic Plastic Salad", 67.70m },
                    { new Guid("6d4d6076-16c4-48fd-ac1c-fce1f10ee38d"), "Unbranded Cotton Bike", 229.92m },
                    { new Guid("701baaa7-8360-4c1b-ade1-d240d194adb8"), "Tasty Metal Shirt", 271.56m },
                    { new Guid("71a9a0e4-13e7-4d4f-a102-5423f44c1fdb"), "Handcrafted Fresh Bike", 299.03m },
                    { new Guid("74388ab2-32a6-4f57-b1f3-f95c5a5f6c78"), "Gorgeous Concrete Cheese", 80.33m },
                    { new Guid("77368e24-abbb-4cb1-a432-11c9352b5887"), "Practical Plastic Car", 256.04m },
                    { new Guid("79a0464e-e29a-41db-9165-58e79257e4e6"), "Ergonomic Cotton Pants", 298.26m },
                    { new Guid("7af68bbd-076c-43f3-9205-50ea4e43c18e"), "Small Soft Mouse", 153.14m },
                    { new Guid("7df44802-f31f-4e99-85b9-59f255bb8289"), "Small Frozen Ball", 210.09m },
                    { new Guid("7e1c12e2-8792-49b9-b4ed-73303f4c6f58"), "Ergonomic Steel Pants", 290.49m },
                    { new Guid("85688121-f5e2-4c60-a481-43f3c181dd95"), "Tasty Concrete Computer", 272.00m },
                    { new Guid("86835388-271e-4e83-b185-b11691449800"), "Tasty Steel Chips", 260.97m },
                    { new Guid("8719bcca-e774-49f6-ae23-d0a980ed6ea0"), "Sleek Granite Soap", 139.19m },
                    { new Guid("882b0a61-e574-4b9a-a9f1-9d9fd3122f08"), "Licensed Steel Fish", 199.02m },
                    { new Guid("8a5ab7db-99d3-44fd-968a-3c4ad0aa7283"), "Unbranded Fresh Mouse", 201.24m },
                    { new Guid("9a4154a9-095b-431a-ae3a-be7a77980b92"), "Intelligent Frozen Gloves", 298.87m },
                    { new Guid("9b7ca426-f8ea-45dc-b17a-279da58c9edf"), "Refined Concrete Cheese", 32.81m },
                    { new Guid("9e1d26ba-7ebc-4df0-bdba-24f00810384b"), "Fantastic Concrete Gloves", 214.34m },
                    { new Guid("9ea4d005-8581-4a0e-8bc1-4f3b2f6e60ba"), "Small Fresh Pizza", 136.75m },
                    { new Guid("a217ddc8-f42f-4404-a0c2-96cd3d53ea8b"), "Rustic Plastic Car", 175.27m },
                    { new Guid("a45a5d5a-95e7-4d3b-a00e-4de107776435"), "Gorgeous Concrete Salad", 124.41m },
                    { new Guid("aa5c22f9-7ccf-4efb-8086-568128c98db0"), "Rustic Granite Chair", 63.58m },
                    { new Guid("b2c6b6f2-94f7-49c6-8613-e73b3f0234c7"), "Gorgeous Rubber Table", 79.88m },
                    { new Guid("b695a3c4-5f8a-4d97-ad21-1583c3cb4139"), "Rustic Rubber Towels", 95.46m },
                    { new Guid("b765868d-9893-4943-9edb-973b85de9646"), "Refined Metal Sausages", 23.74m },
                    { new Guid("b7c95ddc-441c-4b20-b48b-e5f4a3845c51"), "Intelligent Fresh Pants", 234.42m },
                    { new Guid("b82ae6d1-123e-49a6-b388-b6ba053d0a59"), "Fantastic Steel Soap", 138.97m },
                    { new Guid("b859af88-3b4d-4ce6-91d4-8bc291d3bd98"), "Handcrafted Steel Fish", 278.14m },
                    { new Guid("b85c0593-56e1-4bdb-94a2-53813f820511"), "Intelligent Concrete Towels", 199.82m },
                    { new Guid("b9524775-9cd4-4f99-b714-87bd2bf432f9"), "Fantastic Frozen Computer", 151.05m },
                    { new Guid("bc156849-691d-44a3-87fd-3a1ebca07fdc"), "Intelligent Rubber Shirt", 165.47m },
                    { new Guid("bcf0055d-fdae-4076-82fb-318a382e59c2"), "Tasty Rubber Chips", 275.21m },
                    { new Guid("bd5373dc-392a-437c-9ec9-3aaa69658bb3"), "Incredible Cotton Pizza", 231.04m },
                    { new Guid("be47bc2c-7f0b-4b43-a312-212d47b845ce"), "Tasty Concrete Pizza", 44.24m },
                    { new Guid("c0d63fc5-8487-4ef7-ac76-f8de4bc0e61e"), "Unbranded Fresh Car", 83.01m },
                    { new Guid("c35d2bc4-85eb-48c9-a73a-51ff3b4e0859"), "Small Granite Salad", 266.78m },
                    { new Guid("c374d4be-1e53-4424-bac3-cb922020b450"), "Handcrafted Granite Sausages", 273.49m },
                    { new Guid("c914eb82-8035-49c7-a26d-85a6f9556368"), "Sleek Granite Gloves", 292.00m },
                    { new Guid("cbab9e93-1ea9-4c89-8637-9820bd2b26fe"), "Intelligent Granite Pizza", 292.19m },
                    { new Guid("ccc77ec7-fb47-4840-b802-001bed8c7fa2"), "Fantastic Metal Fish", 69.21m },
                    { new Guid("d49b9f2f-f187-4eb5-9051-ea1e3b1561ad"), "Unbranded Fresh Chips", 243.88m },
                    { new Guid("decd4ded-ffdc-44ad-b657-972a82395b9b"), "Handcrafted Metal Ball", 114.77m },
                    { new Guid("ded8ceb6-e751-4978-91cf-ac6c89bd07cf"), "Generic Fresh Ball", 264.97m },
                    { new Guid("e0abc5c4-2ac1-4bc6-ac17-9c11f2903720"), "Incredible Rubber Bike", 52.13m },
                    { new Guid("e850a698-0c48-451d-aff7-31f6b3307828"), "Awesome Plastic Shoes", 288.25m },
                    { new Guid("e906e157-96c7-4ec4-b941-133f2861bc2f"), "Fantastic Steel Cheese", 270.65m },
                    { new Guid("ea9aa195-06a9-4884-b979-7b76c8a1e79e"), "Awesome Frozen Computer", 247.47m },
                    { new Guid("eb5f64f5-8f0e-4abc-9f2a-95ed1c6aa62e"), "Incredible Frozen Car", 8.69m },
                    { new Guid("ecaaaf16-5308-464b-ae7f-4789c5e107a5"), "Licensed Plastic Shoes", 221.40m },
                    { new Guid("ed2ea081-59ff-4cb5-b5ba-7fd925d13ab8"), "Handcrafted Cotton Salad", 211.59m },
                    { new Guid("ee26bd9b-ad8e-4cc7-9443-c295268050b8"), "Small Fresh Pants", 155.85m },
                    { new Guid("eee3ee1f-d8f1-4b92-bb36-cc1d35d87bcd"), "Awesome Wooden Chair", 78.25m },
                    { new Guid("efd9936e-8560-4824-ad31-fddec0ee2a4b"), "Refined Fresh Mouse", 222.43m },
                    { new Guid("f665d01e-edb5-4955-9e08-7c2edeaa9302"), "Practical Metal Table", 126.82m },
                    { new Guid("f6c9dee5-48e8-48e8-b522-fdda9a485db4"), "Refined Wooden Towels", 172.79m },
                    { new Guid("fa8ca79d-6e5a-48c2-aaff-474b4f51fdfe"), "Awesome Plastic Towels", 9.98m },
                    { new Guid("fd6c7b81-4138-4427-8de8-9ae0c91e9b4b"), "Licensed Wooden Gloves", 37.40m }
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
