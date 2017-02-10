﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Training;
using BikeSharing.DomainLogic;

namespace Trainer.Tests
{

    [TestClass]
    public class WorkoutTests
    {
        User _user;

        private void CreateFemaleAthlete()
        {
            _user = new User(1, "kaseyu", 25, 150, 71, Gender.Female);
            var workout = new Workout(1, new TimeSpan(0, 25, 3), 93,
                                      new DateTime(2017, 2, 8, 17, 11, 0),
                                      _user,
                                      "Bodyweight circuit Wednesday!");
            var workout2 = new BikeWorkout(2, new TimeSpan(0, 32, 8), 153,
                                           new DateTime(2017, 2, 6, 16, 11, 0), 3.01,
                                           _user,
                                           "Run around the lake");
            var workout3 = new BikeWorkout(3, new TimeSpan(2, 16, 34), 151,
                                           new DateTime(2017, 2, 4, 10, 30, 0), 14.27,
                                           _user,
                                           "Biking to Red Hook!");
            _user.AddWorkout(workout, workout2, workout3);
        }

        private void CreateMaleAthlete()
        {
            _user = new User(2, "eweb", 27, 208, 72.5, Gender.Male);
            var workout = new BikeWorkout(1, new TimeSpan(0, 32, 5), 128,
                                      new DateTime(2017, 2, 6, 16, 12, 0),
                                      3.14,
                                      _user,
                                      "Running with the puppy!");
            var workout2 = new Workout(1, new TimeSpan(0, 55, 14), 113,
                                      new DateTime(2017, 2, 10, 7, 23, 0),
                                      _user,
                                      "Pumping iron.");
            _user.AddWorkout(workout, workout2);
        }

        [TestMethod]
        public void TestAddWorkoutFemale()
        {
            CreateFemaleAthlete();
            Assert.AreEqual(_user.Workouts.Count, 3);
        }

        [TestMethod]
        public void TestAddAnotherWorkoutFemale()
        {
            CreateFemaleAthlete();
            var w = new Workout(4, new TimeSpan(0, 14, 23), 85, new DateTime(2017, 2, 10), _user, "meh workout");
            _user.AddWorkout(w);
            Assert.AreEqual(_user.Workouts.Count, 4);
        }

        [TestMethod]
        public void TestAddWorkoutMale()
        {
            CreateMaleAthlete();
            Assert.AreEqual(_user.Workouts.Count, 2);
        }

        [TestMethod]
        public void TestHealthStatusFemale()
        {
            CreateFemaleAthlete();
            var score = _user.GetWeekHealthStatus();
            var avg = (93 + 153 + 151) / 3.0;
            var duration = new TimeSpan(3, 13, 45).TotalHours;
            var nominator = (-20.4022 + (0.4472 * avg) - (0.1263 * _user.Weight) + (0.074 * _user.Age) / 4.184) * 60 * duration;
            var actual = nominator / _user.BMR;
            Assert.AreEqual(score, actual, 0.00000001);
        }

        [TestMethod]
        public void TestHealthStatusMale()
        {
            CreateMaleAthlete();
            var score = _user.GetWeekHealthStatus();
            var avg = (128 + 113) / 2.0;
            var duration = new TimeSpan(1, 27, 19).TotalHours;
            var nominator = (-55.0969 + (0.6309 * avg) + (0.1988 * _user.Weight) + (0.2017 * _user.Age) / 4.184) * 60 * duration;
            var actual = nominator / _user.BMR;
            Assert.AreEqual(score, actual, 0.00000001);
        }
    }
}