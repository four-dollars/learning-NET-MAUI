using System.Globalization;
using System.Text;
namespace Core;
public static class PhonewordTranslator
{
    public static string? ToNumber(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;

        raw = raw.ToUpperInvariant(); //將使用者輸入轉為大寫

        var newNumber = new StringBuilder();
        foreach (var c in raw) //尋訪使用者輸入的所有字元
        {
            if (" -0123456789".Contains(c)) { newNumber.Append(c); } //如果尋訪到的字元為- or 0~9，則接於字串newNumber後方
            else
            {
                var result = TranslateToNumber(c); //將字元轉為對應的數字
                if (result != null) //字元為字母
                {
                    newNumber.Append(result); //接於字串後方
                }
                else { return null; }
            }
        }
        return newNumber.ToString(); //回傳值 = 轉換後的數字字串
    }

    static readonly string[] digits =
    {
        "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
    };

    static int? TranslateToNumber(char c)
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i].Contains(c)) return 2 + i; //A, B, C = 2+0 = 2
        }
        return null;
    }
}