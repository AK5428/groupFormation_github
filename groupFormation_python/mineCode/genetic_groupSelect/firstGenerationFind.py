"""
Used to get a random queue list.
"""
from mineCode import save
import copy
import itertools
import random
from mineCode.dataStore import currentProgressWrite

def findFirstGeneration():
    # create a students' list to pick from.
    stuNumList = stuNumListCreate(len(save.list_student))
    # the list used to store
    allQueues = []

    for i in range(save.loopTime):
        # find one queue
        singlePossibility = singleQueueFind(stuNumList)

        allQueues.append(singlePossibility)
        save.currentProcess = len(allQueues) / save.totalAmount * 100
        currentProgressWrite()

        print('Current length: ', len(allQueues))
        print('\ncurrent process: ', save.currentProcess, '%')

    return allQueues

def singleQueueFind(stuNumList):
    # used to store all the groups, equals one queue.
    groupList = []
    # copy one, so the list can be used again.
    thisStuList = copy.deepcopy(stuNumList)
    for size in range(save.groupNum):
        # find all the possibilities for one group
        iterList = list(itertools.combinations(thisStuList, save.minPNum))
        # judge it
        newGroup = random.choice(iterList)

        # if qualify
        groupList.append(newGroup)
        # delete the ones that have already been chosen
        stuDelete(newGroup, thisStuList)

    print('Single possibility:')
    print(groupList)
    return groupList

def stuDelete(newGroup, thisStuList):
    """
    Used to delete the students that have already been chosen.
    :param newGroup:
    :param thisStuList:
    :return:
    """
    for stu in newGroup:
        thisStuList.remove(stu)

def stuNumListCreate(memberSum):
    """
    Used to create the student single number list.
    :param memberSum: the sum of the students.
    :return: the list.
    """
    stuNumList = []
    for num in range(memberSum):
        stuNumList.append(num)
    # print(stuNumList)
    return stuNumList
