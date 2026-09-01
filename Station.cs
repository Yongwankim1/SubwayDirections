public class Station
{
    public string Name;
    public string To;
    public string From;
    public int[] Lines; 
    public int[] SegmentTimes; // 다음역 시간, 이전역 시간

    public Station(string to, string from, int[] lines, int[] segmentTimes)
    {
        To = to;
        From = from;
        Lines = lines;
        SegmentTimes = segmentTimes;
    }

    public void AddLine(int line)
    {
        if(Lines.Length == 0 || Lines == null) return;

        int[] lines = new int[Lines.Length + 1];

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == Lines.Length)
            {
                lines[i] = line;
                break;
            }
            lines[i] = Lines[i];
        }
    }
}