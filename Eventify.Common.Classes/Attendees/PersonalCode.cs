using System.Linq;

namespace Eventify.Common.Classes.Attendees
{
    public interface IPersonalCode
    {
        string Code { get; }

        bool IsValid();
    }

    public class PersonalCode : IPersonalCode
    {
        public string Code { get; }

        public PersonalCode(string code)
        {
            Code = code;
        }

        public bool IsValid()
        {
            var firstDigit = (int)char.GetNumericValue(Code[0]);
            if (Code.Length != 11 || firstDigit is < 1 or > 6 || GetMonth() is < 1 or > 12 || GetDay() is < 1 or > 31)
            {
                return false;
            }

            return GetCheckDigit() == (int)char.GetNumericValue(Code[^1]);
        }


        private int GetMonth()
        {
            return int.Parse($"{Code[3]}{Code[4]}");
        }

        private int GetDay()
        {
            return int.Parse($"{Code[5]}{Code[6]}");
        }

        private int GetCheckDigit()
        {
            var remainder = GetRemainder(1);
            if (remainder == 10)
            {
                remainder = GetRemainder(3);
            }

            return remainder == 10 ? 0 : remainder;
        }

        private int GetRemainder(int startIndex)
        {
            return Code[..10].ToCharArray()
                .Select((x, i) => new { value = (int)char.GetNumericValue(x), index = i + startIndex > 9 ? i + startIndex - 9 : i + startIndex })
                .Sum(x => x.value * x.index) % 11;
        }
    }
}
