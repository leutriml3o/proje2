using Microsoft.EntityFrameworkCore.Migrations;

namespace Sport.Domain.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.Food",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CaloriValue = table.Column<double>(nullable: false),
                    ProteinValue = table.Column<double>(nullable: false),
                    CarbohydrateValue = table.Column<double>(nullable: false),
                    OilValue = table.Column<double>(nullable: false),
                    FiberValue = table.Column<double>(nullable: false),
                    FoodPhoto = table.Column<string>(nullable: true),
                    EnumFoodType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.Movement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementName = table.Column<string>(nullable: true),
                    MovementPhoto = table.Column<string>(nullable: true),
                    MovementDescription = table.Column<string>(nullable: true),
                    EnumMovementType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.Movement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.NutritionList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TotalCaloriValue = table.Column<double>(nullable: false),
                    EnumNutritionType = table.Column<int>(nullable: false),
                    EnumNutritionKind = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.NutritionList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.SportList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EnumSportType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.SportList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.MMRelation.UserNutritionLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSecret = table.Column<string>(nullable: true),
                    FKNutritionListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.MMRelation.UserNutritionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.MMRelation.UserNutritionLists_Sport.Domain.Entities.NutritionList_FKNutritionListId",
                        column: x => x.FKNutritionListId,
                        principalTable: "Sport.Domain.Entities.NutritionList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.NutritionDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FKNutritionListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.NutritionDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.NutritionDay_Sport.Domain.Entities.NutritionList_FKNutritionListId",
                        column: x => x.FKNutritionListId,
                        principalTable: "Sport.Domain.Entities.NutritionList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.MMRelation.UserSportLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSecret = table.Column<string>(nullable: true),
                    FKSportListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.MMRelation.UserSportLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.MMRelation.UserSportLists_Sport.Domain.Entities.SportList_FKSportListId",
                        column: x => x.FKSportListId,
                        principalTable: "Sport.Domain.Entities.SportList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.SportDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FKSportListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.SportDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.SportDay_Sport.Domain.Entities.SportList_FKSportListId",
                        column: x => x.FKSportListId,
                        principalTable: "Sport.Domain.Entities.SportList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.ThatDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FKNutritionDayId = table.Column<int>(nullable: false),
                    EnumMealType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.ThatDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.ThatDay_Sport.Domain.Entities.NutritionDay_FKNutritionDayId",
                        column: x => x.FKNutritionDayId,
                        principalTable: "Sport.Domain.Entities.NutritionDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FKDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.Area_Sport.Domain.Entities.SportDay_FKDayId",
                        column: x => x.FKDayId,
                        principalTable: "Sport.Domain.Entities.SportDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.MMRelation.MealFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKFoodId = table.Column<int>(nullable: false),
                    FKThatDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.MMRelation.MealFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.MMRelation.MealFoods_Sport.Domain.Entities.Food_FKFoodId",
                        column: x => x.FKFoodId,
                        principalTable: "Sport.Domain.Entities.Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.MMRelation.MealFoods_Sport.Domain.Entities.ThatDay_FKThatDayId",
                        column: x => x.FKThatDayId,
                        principalTable: "Sport.Domain.Entities.ThatDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sport.Domain.Entities.MMRelation.AreaMovements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKAreaId = table.Column<int>(nullable: false),
                    FKMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport.Domain.Entities.MMRelation.AreaMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.MMRelation.AreaMovements_Sport.Domain.Entities.Area_FKAreaId",
                        column: x => x.FKAreaId,
                        principalTable: "Sport.Domain.Entities.Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sport.Domain.Entities.MMRelation.AreaMovements_Sport.Domain.Entities.Movement_FKMovementId",
                        column: x => x.FKMovementId,
                        principalTable: "Sport.Domain.Entities.Movement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.Area_FKDayId",
                table: "Sport.Domain.Entities.Area",
                column: "FKDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.MMRelation.AreaMovements_FKAreaId",
                table: "Sport.Domain.Entities.MMRelation.AreaMovements",
                column: "FKAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.MMRelation.AreaMovements_FKMovementId",
                table: "Sport.Domain.Entities.MMRelation.AreaMovements",
                column: "FKMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.MMRelation.MealFoods_FKFoodId",
                table: "Sport.Domain.Entities.MMRelation.MealFoods",
                column: "FKFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.MMRelation.MealFoods_FKThatDayId",
                table: "Sport.Domain.Entities.MMRelation.MealFoods",
                column: "FKThatDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.MMRelation.UserNutritionLists_FKNutritionListId",
                table: "Sport.Domain.Entities.MMRelation.UserNutritionLists",
                column: "FKNutritionListId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.MMRelation.UserSportLists_FKSportListId",
                table: "Sport.Domain.Entities.MMRelation.UserSportLists",
                column: "FKSportListId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.NutritionDay_FKNutritionListId",
                table: "Sport.Domain.Entities.NutritionDay",
                column: "FKNutritionListId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.SportDay_FKSportListId",
                table: "Sport.Domain.Entities.SportDay",
                column: "FKSportListId");

            migrationBuilder.CreateIndex(
                name: "IX_Sport.Domain.Entities.ThatDay_FKNutritionDayId",
                table: "Sport.Domain.Entities.ThatDay",
                column: "FKNutritionDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.MMRelation.AreaMovements");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.MMRelation.MealFoods");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.MMRelation.UserNutritionLists");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.MMRelation.UserSportLists");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.Area");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.Movement");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.Food");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.ThatDay");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.SportDay");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.NutritionDay");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.SportList");

            migrationBuilder.DropTable(
                name: "Sport.Domain.Entities.NutritionList");
        }
    }
}
