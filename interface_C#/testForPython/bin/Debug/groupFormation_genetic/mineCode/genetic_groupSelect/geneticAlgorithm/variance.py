"""
Used as the step3 of the genetic algorithm.
Variance the genes using the probabilities set in the save.
"""
from mineCode import save
import random
import numpy as np
from mineCode.genetic_groupSelect.geneticAlgorithm.queueClass import Queue

def varianceStart():
    """
    The function used to variance the sons.
    :return:
    """

    """if this individual should be variance"""
    # random to see
    bestSonNum = bestQueueFind()
    for sonNum in range(len(save.list_queueSon)):
        # keep the best one
        if sonNum == bestSonNum:
            continue
        son = save.list_queueSon[sonNum]
        # if the chance, start to variance this one
        if random.random() < save.chance4Individual:
            save.individualChanged += 1
            # get the list from the object
            groupList = son.groupList
            flattenList = np.array(groupList).flatten()
            # print(flattenList)
            """if the gene should be variance"""
            for thisGeneNum in range(len(flattenList)):
                if random.random() < save.chance4Gene:
                    save.geneChanged += 1
                    # get the id of the pair gene for exchange
                    pairGeneNum = random.randint(0, len(flattenList) - 1)
                    while pairGeneNum == thisGeneNum:
                        pairGeneNum = random.randint(0, len(flattenList) - 1)

                    # exchange the genes
                    pairGene = flattenList[pairGeneNum]
                    thisGene = flattenList[thisGeneNum]
                    flattenList[thisGeneNum] = pairGene
                    flattenList[pairGeneNum] = thisGene

            """update the son"""
            # change the flattenList into group list again
            newGroupList = np.array(flattenList).reshape((save.minPNum, -1), order='F').T
            newSon = Queue(newGroupList)
            save.list_queueSon[sonNum] = newSon

    print('Done for generation ', save.currentGeneration)
    print('Changed individuals: ', save.individualChanged)
    print('Changed genes: ', save.geneChanged)

def bestQueueFind():
    """
    Find the best queue.
    :return: the best queue.
    """
    maxValue = -1
    bestSonNum = -1
    for sonNum in range(len(save.list_queueSon)):
        queue = save.list_queueSon[sonNum]
        value = queue.finalValue
        if value > maxValue:
            maxValue = value
            bestSonNum = sonNum
    return bestSonNum
