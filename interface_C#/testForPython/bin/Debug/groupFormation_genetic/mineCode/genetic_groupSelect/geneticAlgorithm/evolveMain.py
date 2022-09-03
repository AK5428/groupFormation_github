"""
Used as the main controller of the evolve process, after the first generation already been chosen.
"""
from mineCode.genetic_groupSelect.geneticAlgorithm.queueClass import Queue
from mineCode import save
from mineCode.genetic_groupSelect.geneticAlgorithm.individualChoose import individualChooseStart
from mineCode.genetic_groupSelect.geneticAlgorithm.cross import crossStart
from mineCode.genetic_groupSelect.geneticAlgorithm.variance import varianceStart
from mineCode.dataStore import currentProgressWrite

def evolveStart(queueList):
    for i in range(len(queueList)):
        queueObject = Queue(queueList[i])
        save.list_queueParent.append(queueObject)

    # start to evolve the single generations
    bestValueList = []
    bestQueue = None
    for i in range(save.generationNum):
        save.currentGeneration = i
        # start the single evolve
        singleGeneration()
        # print the best one
        bestQueue = bestQueueFind()
        # currentBestQueue = bestQueue
        bestValueList.append(bestQueue.finalValue)
        print('Best queue on generation ', i)
        print(bestQueue)
        # after each generation, update the list
        save.list_queueParent = save.list_queueSon
        save.list_queueSon = []

        if save.currentProcess < 100:
            save.currentProcess += save.generationCount / save.totalAmount * 100
            currentProgressWrite()

        print('\ncurrent process: ', save.currentProcess, '%')

    save.currentProcess = 100
    currentProgressWrite()
    print('\ncurrent process: ', save.currentProcess, '%')
    print('Generation evolve overview: ', bestValueList)
    return bestQueue

def bestQueueFind():
    """
    Find the best queue.
    :return: the best queue.
    """
    maxValue = -1
    for queue in save.list_queueSon:
        value = queue.finalValue
        if value > maxValue:
            maxValue = value
            bestQueue = queue
    return bestQueue

def singleGeneration():
    # step1, using the Roulette method to choose the part that need to be kept, uncrossed.
    individualChooseStart()
    # step2, randomly cross the left ones.
    crossStart()
    # step3, variance
    varianceStart()


    return