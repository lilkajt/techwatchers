using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Techwatchers.Server.Migrations
{
    /// <inheritdoc />
    public partial class addSamplePosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "posts",
                columns: new[] { "user_id", "category_id", "title", "description", "likes" },
                values: new object[,]
                {
                    { 1, 1, "Najlepszy smartwatch 2025?", "Jakie modele polecacie w tym roku? Chcę coś, co dobrze działa z Androidem.", 12 },
                    { 2, 2, "Problemy z GPS w Galaxy Watch", "Czy ktoś miał problem z dokładnością GPS w Galaxy Watch 5? Macie jakieś rozwiązania?", 8 },
                    { 3, 3, "Nowości od Apple", "Co sądzicie o funkcjach zdrowotnych w najnowszym Apple Watch? Czy EKG rzeczywiście działa?", 15 },
                    { 1, 4, "Bateria w Huawei Watch GT", "Czy to normalne, że bateria trzyma tylko tydzień, mimo że obiecują dwa tygodnie?", 6 },
                    { 2, 5, "Najlepsze aplikacje na Wear OS", "Znacie jakieś aplikacje, które warto zainstalować na smartwatchu z Wear OS?", 20 },
                    { 3, 1, "Funkcje fitness w Garmin", "Zastanawiam się nad Garmin Forerunner. Czy jest wart swojej ceny dla biegacza?", 9 },
                    { 1, 2, "Porównanie Amazfit vs Xiaomi", "Który z tych smartwatchy oferuje lepszy stosunek ceny do jakości? Macie doświadczenia?", 13 },
                    { 2, 3, "Problemy z aktualizacją", "Mój smartwatch przestał działać po ostatniej aktualizacji. Jak to naprawić?", 4 },
                    { 3, 4, "Smartwatch dla dzieci", "Szukam smartwatcha dla dziecka z funkcją lokalizacji. Co polecacie?", 7 },
                    { 1, 5, "Czy warto czekać na nowe modele?", "Czy lepiej kupić teraz, czy poczekać na premiery zapowiedziane na lato?", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM posts WHERE title IN (" +
                "'Najlepszy smartwatch 2025?', 'Problemy z GPS w Galaxy Watch', 'Nowości od Apple', 'Bateria w Huawei Watch GT', " +
                "'Najlepsze aplikacje na Wear OS', 'Funkcje fitness w Garmin', 'Porównanie Amazfit vs Xiaomi', 'Problemy z aktualizacją', " +
                "'Smartwatch dla dzieci', 'Czy warto czekać na nowe modele?')");
        }
    }
}
