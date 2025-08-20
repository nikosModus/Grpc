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
            { "SwitchLanguage", "Greek" },
            { "Undo", "Undo" },
            { "Redo", "Redo" },

        };

        public static Dictionary<string, string> Greek = new Dictionary<string, string>
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
            { "SwitchLanguage", "Αγγλικά" },
            { "SearchLabel", "Αναζήτηση με Επώνυμο" },
            { "SearchByName", "Αναζήτηση με Όνομα" },
            { "SearchBySurname", "Αναζήτηση με Επώνυμο" },
            { "Export", "Εξαγωγή" },
            { "Import", "Εισαγωγή" },
            { "Undo", "Αναίρεση" },
            { "Redo", "Επανάληψη" },

        };

    }
}
