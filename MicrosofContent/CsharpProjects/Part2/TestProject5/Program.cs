int examAssignments = 5;

string[] studentNames = new string[] { "Sophia", "Andrew", "Emma", "Logan" };

int[] sophiaScores = new int[] { 90, 86, 87, 98, 100, 94, 90 };
int[] andrewScores = new int[] { 92, 89, 81, 96, 90, 89 };
int[] emmaScores = new int[] { 90, 85, 87, 98, 68, 89, 89, 89 };
int[] loganScores = new int[] { 90, 95, 87, 88, 96, 96 };

int[] studentScores = new int[10];

string studentLetterGrade = "";

// Console.Clear();
Console.WriteLine("Student\t\tExam Score\tOverall\tGrade\tExtra Credit\n");

foreach (string name in studentNames)
{
    string student = name;

    if (student == "Sophia")
        studentScores = sophiaScores;

    else if (student == "Andrew")
        studentScores = andrewScores;

    else if (student == "Emma")
        studentScores = emmaScores;

    else if (student == "Logan")
        studentScores = loganScores;

    int sumExamScores = 0;
    int sumExtraCreditScores = 0;
    
    int gradedAssignments = 0;
    int extraCreditAssignments = 0;

    decimal studentGrade = 0;
    decimal studentExamScores = 0;
    decimal studentExtraCreditScores = 0;

    decimal gpaPoints = 0;

    foreach (int score in studentScores)
    {
        gradedAssignments += 1;

        if (gradedAssignments <= examAssignments)
            sumExamScores += score;

        else
        {
            extraCreditAssignments += 1;
            sumExtraCreditScores += score;
        }
    }

    studentExamScores = (decimal)sumExamScores / examAssignments;
    studentExtraCreditScores = (decimal)sumExtraCreditScores / extraCreditAssignments;        
    studentGrade = (decimal)((decimal)sumExamScores + ((decimal)sumExtraCreditScores / 10)) / examAssignments;
    gpaPoints = (((decimal)sumExtraCreditScores / 10) / examAssignments);

    if (studentGrade >= 97)
        studentLetterGrade = "A+";

    else if (studentGrade >= 93)
        studentLetterGrade = "A";

    else if (studentGrade >= 90)
        studentLetterGrade = "A-";

    else if (studentGrade >= 87)
        studentLetterGrade = "B+";

    else if (studentGrade >= 83)
        studentLetterGrade = "B";

    else if (studentGrade >= 80)
        studentLetterGrade = "B-";

    else if (studentGrade >= 77)
        studentLetterGrade = "C+";

    else if (studentGrade >= 73)
        studentLetterGrade = "C";

    else if (studentGrade >= 70)
        studentLetterGrade = "C-";

    else if (studentGrade >= 67)
        studentLetterGrade = "D+";

    else if (studentGrade >= 63)
        studentLetterGrade = "D";

    else if (studentGrade >= 60)
        studentLetterGrade = "D-";

    else
        studentLetterGrade = "F";

    // Student         Grade
    // Sophia:         92.2    A-
    
    Console.WriteLine($"{student}\t\t{studentExamScores}" +
        $"\t\t{studentGrade}\t{studentLetterGrade}" +
        $"\t{studentExtraCreditScores} ({gpaPoints} pts.)");
}

// required for running in VS Code (keeps the Output windows open to view results)
Console.WriteLine("\n\rPress the Enter key to continue");
Console.ReadLine();
