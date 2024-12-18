﻿using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.Models
{
    public enum Categories
    {
        GeneralKnowledge,
        Sports,
        Politics,
        Geography,
        History
    }

    public enum Difficulties
    {
        Easy,
        Medium,
        Hard
    }
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        public int CreatorId { get; set; }

        public Categories Category { get; set; }

        public string Title { get; set; }

        public Difficulties Difficulty { get; set; }

        public int MaxPoint { get; set; }

        public int NoOfQuestions { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int Duration { get; set; }

        public bool isEnded  { get; set; } = false;

        public User User { get; set; }

        public LeaderBoard LeaderBoard { get; set; }


    }
}
