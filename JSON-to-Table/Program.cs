// See https://aka.ms/new-console-template for more information


string text = File.ReadAllText("../../../JSON_files/People.json");

int quotationCounter = 0;
List<string> headers = new List<string>();
List<string> rows = new List<string>();
List<string> tableContents = new List<string>();
int tmpIndexEnd = 0;
int tmpIndexStart = 0;


for (int i = 0; i < text.Length; i++)
{
    if (text[i] == ':')
    {
        for (int j = i - 1; j >= 0; j--) //headers are before colons so we search backwards
        {
            if (text[j] == '\"') //headers are inside quotation marks
            {
                quotationCounter++; //there are two quotation marks
                if (quotationCounter == 1)
                    tmpIndexEnd = j; //header ends on this index

                if (quotationCounter == 2)
                {
                    tmpIndexStart = j + 1; //header starts on this index
                    headers.Add(text[tmpIndexStart..tmpIndexEnd].ToUpperInvariant());
                    quotationCounter = 0;
                    break;
                }
            }
        }
        for (int x = i; x < text.Length; x++) //table informations are placed after colons so we search normally
        {
            if (text[x] == '\"') //informations are inside quotation marks
            {
                quotationCounter++; //there are two quotation marks
                if (quotationCounter == 1)
                    tmpIndexStart = x + 1; //information starts on this index

                if (quotationCounter == 2)
                {
                    tmpIndexEnd = x; //information ends on this index
                    tableContents.Add(text[tmpIndexStart..tmpIndexEnd]);
                    quotationCounter = 0;
                    break;
                }
            }
        }
    }
}

headers = headers.Distinct().ToList();  //getting rid of duplicates

for (int i = 0; i < headers.Count; i++)  //adding brackets
{
    if (i == 0)  //at the begining there should be a bracket at the left side
        headers[i] = "|" + headers[i].PadRight(20) + "";

    headers[i] = "" + headers[i].PadRight(20) + "|";  //next on the right sides
    if (i == headers.Count - 1) //and at the end there should be a new line
    {
        headers[i] += "\n";
        for (int j = 0; j < headers.Count * 20 + headers.Count + 1; j++)  //adding horizontal line seperating rows
        {
            headers[i] += "-";
        }
        headers[i] += "\n";
    }
}

int tmp = 0;
for (int i = 0; i < tableContents.Count; i++)  //adding brackets
{
    if (tmp == 0)  //at the begining there should be a bracket at the left side
        tableContents[i] = "|" + tableContents[i].PadRight(20) + "";

    tmp++;
    tableContents[i] = "" + tableContents[i].PadRight(20) + "|";  //next on the right sides
    if (tmp == headers.Count) //and at the end there should be a new line
    {
        tableContents[i] += "\n";
        for (int j = 0; j < headers.Count * 20 + headers.Count + 1; j++)  //adding horizontal line seperating rows
        {
            tableContents[i] += "-";
        }
        tableContents[i] += "\n";
        tmp = 0;
    }
}

//DISPLAYING TABLE
headers.ForEach(x => Console.Write(x));
tableContents.ForEach(x => Console.Write(x));
Console.WriteLine();
