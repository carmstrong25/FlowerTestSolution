using FlowerTestApp.Data.Interfaces;
using FlowerTestApp.Data.Model;
using FlowerTestApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerTestApp.Controllers
{
    public class FlowerController : Controller
    {
        private readonly IFlowerRepository _flowerRepository;
        static List<Flower> listFlowers = new List<Flower>();
        #region Statics
        static int FlowerCount;
        static double Weight1;
        static double Weight2;
        static double Bias;
        static double Result;
        static double Accuracy;
        #endregion

        public FlowerController(IFlowerRepository flowerRepository)
        {
            _flowerRepository = flowerRepository;
        }

        [Route("Flower")]
        public IActionResult List()
        {
            var flowerVM = new List<FlowerViewModel>();
            var flowers = _flowerRepository.GetAll();
            listFlowers = flowers.ToList();
            if (flowers.Count() == 0)
            {
                return View("Empty");
            }

            foreach (var flower in flowers)
            {
                flowerVM.Add(new FlowerViewModel
                {
                    Flower = flower
                });
            }

            FlowerCount = flowers.Count();
            updateViewBag();
            return View(flowerVM);
        }

        public void updateViewBag()
        {
            ViewBag.FlowerCount = FlowerCount;
            ViewBag.Weight1 = Weight1;
            ViewBag.Weight2 = Weight2;
            ViewBag.Bias = Bias;
            ViewBag.Result = Result;
            ViewBag.Accuracy = Accuracy;
        }

        public IActionResult Delete(int id)
        {
            var flower = _flowerRepository.GetById(id);

            _flowerRepository.Delete(flower);

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return View(flower);
            }

            _flowerRepository.Create(flower);

            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var flower = _flowerRepository.GetById(id);

            return View(flower);
        }

        [HttpPost]
        public IActionResult Update(Flower flower)
        {
            if (!ModelState.IsValid)
            {
                return View(flower);
            }

            _flowerRepository.Update(flower);

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Train()
        {
            double weight1 = GetRandomGaussian();
            double weight2 = GetRandomGaussian();
            double bias = GetRandomGaussian();

            double Iterations = 100000;
            double LearningRate = 0.1;

            for (int i = 0; i < Iterations; i++)
            {
                var flowers = _flowerRepository.GetAll();

                Random rnd = new Random();
                int randonInt = rnd.Next(0, flowers.Count());
                Flower randomFlower = flowers.ElementAt(randonInt);

                double zAxis = randomFlower.PedalLength * weight1 + randomFlower.PedalWidth * weight2 + bias;
                double prediction = Sigmoid(zAxis);

                double target = randomFlower.Type;

                double dcost_dpred = 2 * (prediction - target);
                double dpred_dz = Sigmoid_p(zAxis);

                double dz_dw1 = randomFlower.PedalLength;
                double dz_dw2 = randomFlower.PedalWidth;
                double dz_db = 1;

                double dcost_dz = dcost_dpred * dpred_dz;

                double dcost_dw1 = dcost_dz * dz_dw1;
                double dcost_dw2 = dcost_dz * dz_dw2;
                double dcost_db = dcost_dz * dz_db;

                weight1 = weight1 - LearningRate * dcost_dw1;
                weight2 = weight2 - LearningRate * dcost_dw2;
                bias = bias - LearningRate * dcost_db;
            }

            Weight1 = weight1;
            Weight2 = weight2;
            Bias = bias;
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Run_Test(double Len, double Wid)
        {
            double zValue = Weight1 * Len + Weight2 * Wid + Bias;
            double prediction = Sigmoid(zValue);
            Accuracy = prediction;
            if (prediction < .5)
            {
                Result = 0;
            }
            else
            {
                Result = 1;
            }
            return RedirectToAction("List");
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private double Sigmoid_p(double x)
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        }

        private double GetRandomGaussian()
        {
            Random rand = new Random();
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-1.0 * Math.Log(u1)) *
                         Math.Sin(1.0 * Math.PI * u2);
            double randNormal = 0 + 1 * randStdNormal;
            return randNormal;
        }
    }
}
