#include "SimpleMinimization.h"

#include <sys/time.h>
#include <iostream>
#include <stdlib.h>
#include <math.h>
#include <omp.h>

const double DR_SHRINK = 0.8;

using namespace std;

SimpleMinimization::SimpleMinimization(Function *f, double timeLimit) : Minimization(f, timeLimit) {

	generateRandomPosition();

	bestX = x;
	bestY = y;
	bestZ = z;

	bestV = function->value(bestX, bestY, bestZ);

}


SimpleMinimization::~SimpleMinimization() {}


void SimpleMinimization::find(double dr_ini, double dr_fin, int idleStepsLimit) {

	#pragma omp parallel
	{
		int idleSteps;

		double v, dr;
		double x_temp, y_temp, z_temp;
		double x_new, y_new, z_new, v_new;
		double random_x, random_y, random_z;
		double bestX_temp, bestY_temp, bestZ_temp, bestV_temp;

		std::vector<Point3D *> history_temp;

		drand48_data *randomBuffer = new drand48_data;
		int thread_id = omp_get_thread_num();
		srand48_r(thread_id * 69 + 69, randomBuffer);

		drand48_r(randomBuffer, &random_x);
		drand48_r(randomBuffer, &random_y);
		drand48_r(randomBuffer, &random_z);

		while (hasTimeToContinue()) {

			// inicjujemy losowo polozenie startowe w obrebie kwadratu o bokach od min do max
			x_temp = random_x * (maxX - minX) + minX;
			y_temp = random_y * (maxY - minY) + minY;
			z_temp = random_z * (maxZ - minZ) + minZ;

			v = function->value(x_temp, y_temp, z_temp);

			idleSteps = 0;
			dr = dr_ini;

			while ( ( dr > dr_fin ) && ( idleSteps < idleStepsLimit ) ) {

				drand48_r(randomBuffer, &random_x);
				drand48_r(randomBuffer, &random_y);
				drand48_r(randomBuffer, &random_z);

				x_new = x_temp + (random_x - 0.5) * dr;
				y_new = y_temp + (random_y - 0.5) * dr;
				z_new = z_temp + (random_z - 0.5) * dr;

				// upewniamy sie, ze nie opuscilismy przestrzeni poszukiwania rozwiazania
				x_new = limitX(x_new);
				y_new = limitY(y_new);
				z_new = limitZ(z_new);

				// wartosc funkcji w nowym polozeniu
				v_new = function->value(x_new, y_new, z_new);

				if (v_new < v) {

					x_temp = x_new;  // przenosimy sie do nowej, lepszej lokalizacji
					y_temp = y_new;
					z_temp = z_new;

					v = v_new;

					idleSteps = 0; // resetujemy licznik krokow, bez poprawy polozenia

				} else {

					idleSteps++; // nic sie nie stalo

					if ( idleSteps == idleStepsLimit ) {

						dr *= DR_SHRINK; // zmniejszamy dr
						idleSteps = 0;

					}
				}
			} // dr wciaz za duze

			// Mimic the usage of addToHistory but with the temporary coord. values
			auto * point = new Point3D(x_temp, y_temp, z_temp);

			history_temp.push_back(point);

			if (bestV_temp > v) {

				bestX_temp = x_temp;
				bestY_temp = y_temp;
				bestZ_temp = z_temp;

				bestV_temp = v;
			}
		}

		// Handle the temporarily-saved variables AFTER the while loops
		#pragma omp critical
		{
			for (auto point_temp : history_temp) {

				x = point_temp->getX();
				y = point_temp->getY();
				z = point_temp->getZ();

				addToHistory();
			}

			if (bestV > bestV_temp) {

				bestX = bestX_temp;
				bestY = bestY_temp;
				bestZ = bestZ_temp;

				bestV = bestV_temp;
			}

		}
	}
}

void SimpleMinimization::generateRandomPosition() {
    x = drand48() * (maxX - minX) + minX;
    y = drand48() * (maxY - minY) + minY;
    z = drand48() * (maxZ - minZ) + minZ;
}
