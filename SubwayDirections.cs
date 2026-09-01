using System;

public class SubwayDirections
{
    public static Dictionary<string, Station> Stations = new Dictionary<string, Station>();

    public int CurrentLine = -999;
    static void Main(string[] args)
    {
        Init();
        Console.Write("출발 역 : ");
        string StartName = Console.ReadLine();
        Console.Write("도착 역 : ");
        string EndName = Console.ReadLine();

        FindShortPath(StartName, EndName);
    }

    private static void FindStationInfo(string stationName)
    {
        if (string.IsNullOrWhiteSpace(stationName))
            return;

        string findStation = stationName.Replace(" ", "");

        if (!Stations.TryGetValue(findStation, out Station station))
        {
            Console.WriteLine("해당 역을 찾을 수 없습니다.");
            return;
        }
        Console.WriteLine($"{findStation}역 정보");

        Console.Write("호선 : ");
        foreach (int line in station.Lines)
        {
            Console.Write($"{line}호선 ");
        }

        Console.WriteLine();
        Console.WriteLine("연결 역");

        foreach (StationData data in station.StationDatas)
        {
            Console.WriteLine($" - {data.To}역 : {data.SegmentTime / 60}분 {data.SegmentTime % 60}초"
            );
        }
    }

    private static void Init()
    {
        string[] railWayStation = { "용산", "남영", "서울역", "시청", "종각", "종로3가", "종로5가", "동대문", "동묘앞", "신설동", "제기동", "청량리" };
        int[] railWaySegmentTime = { 110, 120, 120, 100, 90, 90, 90, 80, 80, 90, 100 };
        AddStation(railWayStation, railWaySegmentTime, 1);

        railWayStation = new string[] { "당산", "합정", "홍대입구", "신촌", "이대", "아현", "충정로", "시청", "을지로 입구", "을지로3가", "을지로4가", "동대문역사문화공원", "신당", "상왕십리", "왕십리", "한양대" };
        railWaySegmentTime = new int[] { 170, 100, 110, 90, 90, 90, 110, 90, 90, 80, 100, 100, 100, 90, 100 };
        AddStation(railWayStation, railWaySegmentTime, 2);

        railWayStation = new string[] { "경복궁", "안국", "종로3가", "을지로3가", "총무로", "동대입구", "약수", "금호", "옥수" };
        railWaySegmentTime = new int[] { 100, 90, 70, 80, 100, 90, 90, 90 };
        AddStation(railWayStation, railWaySegmentTime, 3);

        railWayStation = new string[] { "이촌", "신용산", "삼각지", "숙대입구", "서울역", "회현", "명동", "총무로", "동대문역사문화공원", "동대문", "혜화" };
        railWaySegmentTime = new int[] { 100, 90, 100, 100, 90, 90, 80, 100, 90, 90 };
        AddStation(railWayStation, railWaySegmentTime, 4);

        railWayStation = new string[] { "마포", "공덕", "애오개", "충정로", "서대문", "광화문", "종로3가", "을지로4가", "동대문역사문화공원", "청구", "신금호", "행당", "왕십리", "마장" };
        railWaySegmentTime = new int[] { 100, 110, 100, 90, 120, 100, 90, 90, 100, 100, 100, 100, 100 };
        AddStation(railWayStation, railWaySegmentTime, 5);

        railWayStation = new string[] { "망원", "합정", "상수", "광흥창", "대홍", "공덕", "효창공원앞", "삼각지", "녹사평", "이태원", "한강진", "버티고개", "약수", "청구", "신당", "동묘앞", "창신" };
        railWaySegmentTime = new int[] { 100, 100, 100, 100, 110, 100, 130, 110, 90, 100, 110, 90, 90, 90, 100, 90 };
        AddStation(railWayStation, railWaySegmentTime, 6);
    }


    private static void AddStation(string[] stations, int[] segmentTime, int line)
    {
        if (stations.Length != segmentTime.Length + 1)
        {
            throw new ArgumentException("역의 개수는 구간 시간의 개수보다 1개 많아야 합니다.");
        }
        for (int i = 0; i < stations.Length; i++)
        {
            string stationName = stations[i].Replace(" ", "");

            Station station;

            if (Stations.ContainsKey(stationName))
            {
                station = Stations[stationName];
                station.AddLine(line);
            }
            else
            {
                station = new Station(line);
                Stations.Add(stationName, station);
            }

            if (i > 0)
            {
                station.AddStationData(stations[i-1].Replace(" ",""),segmentTime[i - 1]);
            }
            if(i < stations.Length - 1)
            {
                station.AddStationData(stations[i+1].Replace(" ", ""),segmentTime[i]);
            }
        }
    }

    private static void FindShortPath(string inputStartStation, string inputEndStation)
    {
        string startFindStation = inputStartStation.Replace(" ", "");
        string endFindStation = inputEndStation.Replace(" ", "");

        if (!Stations.ContainsKey(startFindStation))
        {
            Console.WriteLine("출발 역이 존재하지 않는 역입니다.");
            return;
        }
        if (!Stations.ContainsKey(endFindStation))
        {
            Console.WriteLine("도착 역이 존재하지 않는 역입니다.");
            return;
        }

        PriorityQueue<string, int> queue = new PriorityQueue<string, int>();
        Dictionary<string, int> minTime = new Dictionary<string, int>();
        Dictionary<string, string> previous = new Dictionary<string, string>();

        foreach (string stationName in Stations.Keys)
        {
            minTime[stationName] = int.MaxValue;
        }

        minTime[startFindStation] = 0;
        queue.Enqueue(startFindStation, 0);

        while (queue.Count > 0)
        {
            string currentStationName = queue.Dequeue();
            Station currentStation = Stations[currentStationName];

            if(currentStationName.Equals(endFindStation))
            {
                List<string> path = new List<string>();
                string current = endFindStation;
                path.Add(current);

                while (previous.ContainsKey(current))
                {
                    current = previous[current];
                    path.Add(current);
                }

                path.Reverse();
                Console.WriteLine($"[탐색결과], {startFindStation} -> {endFindStation}");
                Console.Write("이동경로 : ");
                foreach (string station in path)
                {
                    Console.Write(station);

                    if (station != path[^1])
                    {
                        Console.Write("->");
                    }
                }
                Console.WriteLine($"\n총 소요 시간 : {minTime[endFindStation]/60}분 {minTime[endFindStation]%60}초");
            }

            foreach(StationData next in currentStation.StationDatas)
            {
                int newTime = minTime[currentStationName] + next.SegmentTime;

                if(newTime < minTime[next.To])
                {
                    minTime[next.To] = newTime;
                    previous[next.To] = currentStationName;
                    queue.Enqueue(next.To, newTime);
                }
            }

        }
    }
}