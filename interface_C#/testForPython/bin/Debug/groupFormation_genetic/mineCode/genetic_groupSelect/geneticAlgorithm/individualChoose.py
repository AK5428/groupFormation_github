"""
Used to calculate the probability of  the individuals, and then choose the higher ones.
This is the first step to build the next generation.
"""
from mineCode import save
import random
import math

def individualChooseStart():
    # store all the values into the list
    valueList = []
    # -1, let the numbers vary more
    # calculate the fix number
    fixNum = math.floor(save.list_queueParent[bestQueueFind()].finalValue)
    for queue in save.list_queueParent:
        if queue.finalValue > fixNum:
            valueList.append(queue.finalValue - fixNum)
        else:
            valueList.append(0)

    # calculate the proportion
    valueSum = sum(valueList)
    # simple proportion
    proportionList = []
    # the position on the line
    positionList = []
    # initial the position
    position = 0
    # loop to calculate the proportion and its position.
    for value in valueList:
        proportion = value / valueSum
        proportionList.append(proportion)
        position += proportion
        positionList.append(position)

    # print('Position list: \n', positionList)

    # choose start
    randomChoose(positionList)

    print('Done for step1, chosen sum: ', len(save.list_queueSon))


def randomChoose(positionList):
    """
    Random all the numbers and put the
    :param positionList: the individuals' position on the wheel
    :return: the chosen ones.
    """
    # generate the random numbers
    randomList = []
    # print('\nStep1: choose the ones to keep. Sums:', save.chosenIndividualNum)
    # -1, empty the position for the best one
    for i in range(save.chosenIndividualNum - 1):
        randomList.append(random.random())
    randomList.sort()
    # print('\nRandom list: \n', randomList)

    # add the best one first
    save.list_queueSon.append(save.list_queueParent[bestQueueFind()])

    # using the randomList to choose the individuals
    for num in randomList:
        for i in range(len(positionList) - 1):
            if num > 0 and num <= positionList[0]:
                save.list_queueSon.append(save.list_queueParent[0])
                # print('chosen:', i, 0, num, positionList[0])
                break
            elif num > positionList[i] and num <= positionList[i + 1]:
                save.list_queueSon.append(save.list_queueParent[i + 1])
                # print('chosen:', i, i + 1)
                break

def bestQueueFind():
    """
    Find the best queue.
    Best from the parents.
    :return: the best queue.
    """
    maxValue = -1
    bestSonNum = -1
    for sonNum in range(len(save.list_queueParent)):
        queue = save.list_queueParent[sonNum]
        value = queue.finalValue
        if value > maxValue:
            maxValue = value
            bestSonNum = sonNum
    return bestSonNum