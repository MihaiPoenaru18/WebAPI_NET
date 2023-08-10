using System;

public class Program
{
    public static void Main(string[] args)
    {

        Console.Write("Introduceti sirul de caractere A: ");
        string inputStr = Console.ReadLine();

        Console.Write("Introduceti masca M: ");
        string mask = Console.ReadLine();

        string transformedString = ApplyMask(inputStr, mask);
        if (transformedString != null)
        {
            Console.WriteLine("Sirul rezultat este: " + transformedString);
        }
    }

    public static bool ApplyMaskException(string text, string mask)
    {
        if (text == null && mask == null)
        {
            Console.WriteLine("Nici un sir si nici o masca nu au fost introduse!!");
            throw new ArgumentNullException(text, mask);
        }

        if (text == null)
        {
            Console.WriteLine("Nici un sir nu a fost introdus!!!");
            throw new ArgumentNullException(text);
        }
        if (text == null)
        {
            Console.WriteLine("Nici un sir nu a fost introdus!!!");
            throw new ArgumentNullException(text);
        }
        return true;
    }

    public static string ApplyMaskForCountOfMaskBigerThanArray(string text, string mask)
    {
        int numberOfX = 0;
        int numberOfY = 0;
        string result = "";
        int inputIndex = 0;
        for (int i = 0; i < mask.Count(); i++)
        {
            if (mask[i] == 'X')
            {
                numberOfX++;
            }
            if (mask[i] == 'Y')
            {
                numberOfY++;
            }
        }

        if (mask.Contains("X"))
        {
            bool addStar = false;
            for (int i = 0; i < mask.Count(); i++)
            {
                if (mask[i] == 'X')
                {
                    result += text[inputIndex];
                    inputIndex++;
                }
                else
                {
                    result += mask[i];
                    inputIndex = 0;
                    if (!addStar)
                    {
                        for (int j = 0; j < numberOfX - text.Count(); j++)
                        {
                            result += "*";
                        }
                        i += numberOfX - text.Count();
                    }
                    addStar = true;
                }
            }
        }
        if (mask.Contains("Y"))
        {
            bool addStar = false;
            for (int i = 0; i < mask.Count(); i++)
            {

                if (mask[i] == 'Y')
                {
                    result += text[inputIndex];
                    inputIndex++;
                }
                else
                {
                    result += mask[i];
                }
                if (inputIndex == text.Count())
                {
                    break;
                }

            }
            for (int j = 0; j < numberOfY - text.Count(); j++)
            {
                result += "*";
            }
        }
        return result;
    }

    public static string ApplyMask(string text, string mask)
    {
        if (!ApplyMaskException(text, mask))
        {
            return null;
        }
        string result = "";
        int inputIndex = 0;

        if (text.Count() == mask.Count())
        {
            for (int i = 0; i < mask.Count(); i++)
            {
                if (mask[i] == 'X' || mask[i] == 'Y')
                {
                    result += text[inputIndex];
                    inputIndex++;
                }
                else
                {
                    result += mask[i];
                }
            }
        }
        if (mask.Count() > text.Count())
        {
            result = ApplyMaskForCountOfMaskBigerThanArray(text, mask);
        }
        if(mask.Count() < text.Count())
        {
            for (int i = 0; i < mask.Count(); i++)
            {
                if (mask[i] == 'X' || mask[i] == 'Y')
                {
                    result += text[inputIndex];
                    inputIndex++;
                }
                else
                {
                    result += mask[i];
                }
            }
        }
        return result;
    }
}