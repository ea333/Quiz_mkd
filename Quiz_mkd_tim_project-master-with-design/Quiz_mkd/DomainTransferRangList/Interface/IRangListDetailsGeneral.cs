using Quiz.Repository.Domain_Transfer;

namespace Quiz.Web.Domain_Transfer.Interface
{
    public interface IRangListDetailsGeneral
    {
        public RangListDetails EventFieldSelectedOnly(int? selectedEventId);

        public RangListDetails CategoryFieldSelectedOnly(int? selectedCategoryId);

        public RangListDetails YearFieldSelectedOnly(int selectedYear);

        public RangListDetails EventAndCategoryFieldSelected(int? selectedEventId, int? selectedCategoryId);

        public RangListDetails EventAndYearFieldSelected(int? selectedEventId, int selectedYear);

        public RangListDetails CategoryAndYearFieldSelected(int? selectedCategoryId, int selectedYear);

        public RangListDetails AllOfTheFiledsSelected(int? selectedEventId, int? selectedCategoryId, int selectedYear);


    }
}
