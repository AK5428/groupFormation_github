"""
Used as the main trigger of the group select process.
Using genetic algorithm as the method.
1. initiate the population, for preparation.
2. start the loop, evolve the population.
    [choose][cross][variation]
    then loop.
"""
from mineCode import save
import time
import numpy as np
from mineCode.genetic_groupSelect.mahaDistance.mahaDistanceTrigger import mahaDistanceTrigger
from mineCode.genetic_groupSelect.firstGenerationFind import findFirstGeneration
from mineCode.genetic_groupSelect.geneticAlgorithm.evolveMain import evolveStart
from mineCode.genetic_groupSelect.resultProcess import resultProcessStart
from mineCode.dataStore import currentProgressWrite

def groupSelectStart(homos,heters,isEquilibrium):
    save.list_queueParent = []
    save.list_queueSon = []

    save.homoDimension = homos
    save.heterDimension = heters
    save.isEquilibrium = isEquilibrium
    # initial the progress
    save.currentProcess = 0
    currentProgressWrite()
    # get the info from before
    preProcessedResult()

    # calculate the mahaDistance
    mahaDistanceTrigger()

    # initiate the first population
    firstGenerationList = findFirstGeneration()
    # print(firstGenerationList)
    print('First generation done.\n')

    # start the genetic algorithm, evolve the populations
    bestQueue = evolveStart(firstGenerationList)
    save.currentBestQueue = bestQueue

    # result process
    resultProcessStart(bestQueue)

    return

def preProcessedResult():
    """
    The result of the preProcess, read the file and get the result.
    :return: the data from before.
    """
    npyName = save.ORIGINAL_FOLDER.split('_')[-1] + '.npy'
    npyPath = save.PRE_PROCESSED_FOLDER + '/' + npyName
    save.list_colony = np.load(npyPath, allow_pickle=True)
    save.list_student = save.list_colony[0].list_students


def printTime(seconds):
    """
    Used to print the run time
    :param seconds:
    :return:
    """
    m, s = divmod(seconds, 60)
    h, m = divmod(m, 60)
    print('\n\n Run time: ', "%02d:%02d:%02d" % (h, m, s))


if __name__ == '__main__':
    # mark the time
    start = time.time()

    # start the process
    groupSelectStart(save.homoDimension, save.heterDimension, save.isEquilibrium)

    # print the used time
    end = time.time()
    runTime = end - start
    printTime(runTime)