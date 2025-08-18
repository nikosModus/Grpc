using System.Collections.Generic;

namespace People.Winforms
{
    public static class Translations
    {
        public static readonly Dictionary<string, string> English = new()
        {
            { "Name", "Name" },
            { "Surname", "Surname" },
            { "Age", "Age" },
            { "DOB", "Date Of Birth" },
            { "Create", "Create Person" },
            { "Update", "Update Person" },
            { "Delete", "Delete Person" },
            { "Clear", "Clear All Fields" },
            { "DarkMode", "Dark Mode" },
            { "SearchLabel", "Search" },
            { "SearchByName", "Search by Name" },
            { "SearchBySurname", "Search by Surname" },
            { "Export", "Export As..." },
            { "Import", "Import..." },
            { "SwitchLanguage", "Greek" }

        };

        public static readonly Dictionary<string, string> Greek = new()
        {
            { "Name", "Όνομα" },
            { "Surname", "Επώνυμο" },
            { "Age", "Ηλικία" },
            { "DOB", "Ημ. Γέννησης" },
            { "Create", "Δημιουργία" },
            { "Update", "Ενημέρωση" },
            { "Delete", "Διαγραφή" },
            { "Clear", "Καθαρισμός" },
            { "DarkMode", "Σκούρο Θέμα" },
            { "SearchLabel", "Αναζήτηση" },
            { "SearchByName", "Με Όνομα" },
            { "SearchBySurname", "Με Επώνυμο" },
            { "Export", "Εξαγωγή" },
            { "Import", "Εισαγωγή" },
            { "SwitchLanguage", "Αγγλικά" },

        };
    }
}
