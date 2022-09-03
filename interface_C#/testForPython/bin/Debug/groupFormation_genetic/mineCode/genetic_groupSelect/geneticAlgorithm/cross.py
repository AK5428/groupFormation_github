"""
The step2 for the genetic algorithm.
One part is filled with steps, choose the better ones directly from the last generation.
The other part need to be found from the cross, randomly choose two individual from the last generation, and then
cross them, store the result as son.
"""
from mineCode import save
import random
import copy
import numpy as np
import math
from mineCode.genetic_groupSelect.geneticAlgorithm.queueClass import Queue

def crossStart():
    # each cross produce 2 sons, so we only need to cross half the time
    for i in range(int(save.crossIndividualNum / 2)):
        # -1, since the list start with 0
        # print('\nround: ', i)
        parentSum = len(save.list_queueParent) - 1
        parentA = random.randint(0, parentSum)
        parentB = random.randint(0, parentSum)
        singleCross(parentA, parentB)
        # break

    print('Done for step2, current sum: ', len(save.list_queueSon))
    return

def singleCross(parentA, parentB):
    # randomly find the cross point
    crossPList = []
    for i in range(save.groupNum):
        crossPoint = random.randint(0, save.minPNum - 1)
        crossPList.append(crossPoint)

    # using the cross points to cross the sons
    # since there are two sons, using different parent as the main one, call twice
    oneParent(parentA, parentB, crossPList)
    oneParent(parentB, parentA, crossPList)

    return

def oneParent(mainParent, crossParent, crossPList):
    # using the parent id to find the queue
    mainPQueue = save.list_queueParent[mainParent].groupList
    crossParent = save.list_queueParent[crossParent].groupList


    """1. keep the self part, from the main parent"""
    newSon = copy.deepcopy(mainPQueue)
    # empty the right gene, filled with the cross parent later
    for i in range(len(newSon)):
        newSon[i] = list(newSon[i])
        group = newSon[i]
        for j in range(crossPList[i] + 1, len(group)):
            group[j] = -1

    # print('Self part: \n', newSon)
    # print(crossParent)

    """2. copy the right gene from the cross parent, except the members that already exists."""
    # set one flatten list, used to judge if there is member repeated
    flattenList = np.array(newSon).flatten()
    for i in range(len(newSon)):
        groupMain = newSon[i]
        groupCross = crossParent[i]
        for j in range(len(groupMain)):
            # using the -1 to find the cross point, and the corresponding num from the cross group
            if groupMain[j] == -1:
                crossNum = groupCross[j]
                # judge if the queue already have this member
                if crossNum not in flattenList:
                    groupMain[j] = crossNum

    # print('Add cross: \n', newSon)

    """3. add the left students"""
    # calculate the ones not in
    # define a list with all the members
    fullList = list(range(0, save.groupNum * save.minPNum, 1))
    # again, the list used to compare
    flattenList = np.array(newSon).flatten()
    # the list that need to be refill
    list_stuRefill = []
    for stu in fullList:
        if stu not in flattenList:
            # add back the student
            list_stuRefill.append(stu)

    # add back these students
    stuAddBack(newSon, mainPQueue, list_stuRefill)

    """create the Queue object for the new son, and then saved into list in save.py"""
    newQueue = Queue(newSon)
    save.list_queueSon.append(newQueue)
    # print('Current length of son list:', len(save.list_queueSon))


def stuAddBack(newSon, mainParent, list_stuRefill):
    """
    The func used to add back the ones that are lost.
    Put it back to the original group as priority
    :param newSon: the list of the new son.
    :param mainParent: the main parent.
    :param stu: the student that was lost.
    :return: none
    """
    """find the original place"""
    # the list used to find the place where the student originally is.
    flattenList = np.array(mainParent).flatten()
    # the student list, that cannot fill in the original group
    list_cannotFill = copy.deepcopy(list_stuRefill)
    # try to fill them
    for stu in list_stuRefill:
        groupPosition = math.floor(list(flattenList).index(stu) / save.minPNum)
        if -1 in newSon[groupPosition]:
            index = list(newSon[groupPosition]).index(-1)
            newSon[groupPosition][index] = stu
            list_cannotFill.remove(stu)
    # print('Fill with self position: \n', newSon)
    # print(mainParent)

    # for the ones that cannot fill in the original group, add in sort
    for group in newSon:
        for i in range(len(group)):
            if group[i] == -1:
                group[i] = list_cannotFill.pop()

    # print('Add the last ones: \n', newSon)













