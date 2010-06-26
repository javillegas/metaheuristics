using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GANPS42SP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double popFactor = 50;
		protected double mutProbability = 0.3;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			
			// Setting the parameters of the GA for this instance of the problem.
			int popSize = (int) Math.Ceiling(popFactor * instance.NumberItems);
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberItems - 1;
			}
			DiscreteGA genetic = new DiscreteGANPS42SP(instance, popSize, mutProbability, lowerBounds, upperBounds);

			// Solving the problem and writing the best solution found.
			genetic.Run(timeLimit);
			int[,] coordinates = TwoSPUtils.NPSCoordinates(instance, genetic.BestIndividual);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "GA using the NPS heuristic for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.GA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}

		public void UpdateParameters(double[] parameters)
		{
			popFactor = parameters[0];
			mutProbability = parameters[1];
		}
	}
}