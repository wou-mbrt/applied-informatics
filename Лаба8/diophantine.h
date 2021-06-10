#include <stdlib.h>
#include <time.h>

#define MAXPOP	25

struct gene {
	int alleles[4];
	int fitness;
	float likelihood;

	// Test for equality.
	int operator==(gene gn) {
		for (int i=0;i<4;i++) {
			if (gn.alleles[i] != alleles[i]) return false;
		}

		return true;
	}
};

class CDiophantine {
	public:
		CDiophantine(int, int, int, int, int);		// Конструктор с коэффициентами a, b, c, d
		int Solve();
		
		// Returns a given gene.
		gene GetGene(int i) { return population[i];}

	protected:
		int ca,cb,cc,cd;
		int result;
		gene population[MAXPOP];					// Популяция

		int Fitness(gene &);
		void GenerateLikelihoods();
		float MultInv();
		int CreateFitnesses();
		void CreateNewPopulation();
		int GetIndex(float val);

		gene Breed(int p1, int p2);

};

CDiophantine::CDiophantine(int a, int b, int c, int d, int res) : ca(a), cb(b), cc(c), cd(d), result(res) {}

int CDiophantine::Solve() {
	int fitness = -1;

	// Generate initial population.
	srand((unsigned)time(NULL));

	for(int i=0;i<MAXPOP;i++) {
		for (int j=0;j<4;j++) {
			population[i].alleles[j] = rand() % (result + 1);
		}
	}

	if (fitness = CreateFitnesses()) {
		return fitness;
	}

	int iterations = 0;
	while (fitness != 0 || iterations < 50) {
		GenerateLikelihoods();
		CreateNewPopulation();
		if (fitness = CreateFitnesses()) {
			return fitness;
		}
		
		iterations++;
	}

	return -1;
}

int CDiophantine::Fitness(gene &gn) {
	int total = ca * gn.alleles[0] + cb * gn.alleles[1] + cc * gn.alleles[2] + cd * gn.alleles[3];

	return gn.fitness = abs(total - result);
}

int CDiophantine::CreateFitnesses() {
	float avgfit = 0;
	int fitness = 0;
	for(int i=0;i<MAXPOP;i++) {
		fitness = Fitness(population[i]);
		avgfit += fitness;
		if (fitness == 0) {
			return i;
		}
	}
	
	return 0;
}

float CDiophantine::MultInv() {
	float sum = 0;

	for(int i=0;i<MAXPOP;i++) {
		sum += 1/((float)population[i].fitness);
	}

	return sum;
}

void CDiophantine::GenerateLikelihoods() {
	float multinv = MultInv();

	float last = 0;
	for(int i=0;i<MAXPOP;i++) {
		population[i].likelihood = last = last + ((1/((float)population[i].fitness) / multinv) * 100);
	}
}

int CDiophantine::GetIndex(float val) {
	float last = 0;
	for(int i=0;i<MAXPOP;i++) {
		if (last <= val && val <= population[i].likelihood) return i;
		else last = population[i].likelihood;
	}
	
	return 4;
}

gene CDiophantine::Breed(int p1, int p2) {
	int crossover = rand() % 3+1;
	int first = rand() % 100;

	gene child = population[p1];

	int initial = 0, final = 3;
	if (first < 50) initial = crossover;
	else final = crossover+1;

	for(int i=initial;i<final;i++) {
		child.alleles[i] = population[p2].alleles[i];
		if (rand() % 101 < 5) child.alleles[i] = rand() % (result + 1);
	}

	return child;
}

void CDiophantine::CreateNewPopulation() {
	gene temppop[MAXPOP];

	for(int i=0;i<MAXPOP;i++) {
		int parent1 = 0, parent2 = 0, iterations = 0;
		while(parent1 == parent2 || population[parent1] == population[parent2]) {
			parent1 = GetIndex((float)(rand() % 101));
			parent2 = GetIndex((float)(rand() % 101));
			if (++iterations > 25) break;
		}
		
		temppop[i] = Breed(parent1, parent2);
	}

	for(int i=0;i<MAXPOP;i++) population[i] = temppop[i];
}
