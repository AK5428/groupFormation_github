"""
Used to actually count the similarities and call the threshold to accelerate the process.
"""
import time

from mineCode import save
import copy
import numpy as np
import mineCode.preProcess.similarCountPackage.tecentSimilar as similar
import math
from threading import Thread

def similarCount_withPair(thisColony):
    """
    Used to calculate similarities after all the pairs are found and save in dict in colony.
    This function is the starter to calculate.
    Read all pairs, calculate into an array, and then save them.
    :return: the similarities.
    """
    # one dimension at a time
    for key in thisColony.dict_tagPair.keys():
        # update the colony dict, prepared to save
        thisColony.dict_tagSimilarities.update({key: {}})
        pairList = list(thisColony.dict_tagPair[key].keys())

        # count the similarities using threshold
        similarCount_threshold(key, pairList, thisColony)
        # print(thisColony.dict_tagSimilarities)

        # if the similarity calculate process gone wrong
        while not save.list_similarFalse == []:
            similarCount_threshold(key, save.list_similarFalse, thisColony)


def similarCount_threshold(dimensionName, pairList, thisColony):
    """
    The func used to start the threshold.
    As it could be used to calculate the false list, should contain the method to renew the false list.
    :param dimensionName: the name and also the key for the current dimension
    :param pairList: the list of the pair, used to determine the slice for each thread
    :param thisColony: the place where the pair stores and the result to be stored
    :return: the calculated similarities
    """
    # get the false list, and then clear the false list
    pairList = copy.deepcopy(pairList)
    save.list_similarFalse = []

    # create the list for threads
    thread_list = []

    # the thread sum
    times = 8

    # work amount for each thread
    split_count = math.ceil(len(pairList) / times)

    count = 0
    for item in range(times):
        splitList = pairList[count: count + split_count]
        # 线程相关处理
        thread = Thread(target=similarCount_SingleThread, args=(dimensionName, splitList, thisColony,))
        thread_list.append(thread)
        # 在子线程中运行任务
        thread.start()
        count += split_count

        # 线程同步，等待子线程结束任务，主线程再结束
    for _item in thread_list:
        _item.join()

    # start the threshold
    # similarCount_SingleThread(dimensionName, pairList, thisColony)
    return

def similarCount_SingleThread(dimensionName, pairList, thisColony):
    # loop the pairlist, and calculate for the current dimension
    for pair in pairList:
        # get both of the lists
        horizonList, verticalList = thisColony.dict_tagPair[dimensionName][pair].values()

        # create an array for it
        # first num of the horizon, last num of the vertical
        similarArray = np.empty((len(horizonList), len(verticalList)))

        # loop to pair each single point and calculate the similarities
        for i in range(len(horizonList)):
            pointHorizon = horizonList[i]
            for j in range(len(verticalList)):
                pointVertical = verticalList[j]
                try:
                    similarDict = similar.similarityCalculate(pointHorizon, [pointVertical])[0]
                    similarArray[i][j] = similarDict['Score']
                except:
                    print('\033[1;35;0m Error in:  \033[0m', '-- ', pair)
                    save.list_similarFalse.append(pair)

                time.sleep(0.3)

        # print and save the similarities data
        print(pair, '-- done')
        thisColony.dict_tagSimilarities[dimensionName].update({pair: similarArray})


    return