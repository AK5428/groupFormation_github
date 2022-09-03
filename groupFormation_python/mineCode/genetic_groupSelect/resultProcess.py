"""
Used to process the final result, the best queue.
1. return the best queue info to the visible part.
2. add a new queue to adopt the former one, then add the function of name and user's choice.
3. store the best queue to best queue list.
4. add a function to process the final list.
    a. write in the new queue.
    b. read the old list.
    c. delete all clear the queue.
"""
from mineCode import save
import numpy as np

# the path of the npy
folderName = save.ORIGINAL_FOLDER.split('_')[-1]
savePath = save.BEST_QUEUE_FOLDER + '/' + folderName + '.npy'

def resultProcessStart(bestQueue):
    # read the list
    oldList = np.array(np.load(savePath, allow_pickle=True))
    # print(save.list_finalResult)

    # add the current best into list
    newList = np.append(oldList, bestQueue)

    # print all the Final results
    np.save(savePath, newList, allow_pickle=True)
    for queue in newList:
        print("\nBest queue: \n", queue, '\n')
    return

def queueDelete(num):
    """
    Used to delete one single result.
    :param num: this result's num from the list.
    :return:
    """
    save.list_finalResult = list(np.load(savePath, allow_pickle=True))
    save.list_finalResult.pop(num)
    np.save(savePath, save.list_finalResult, allow_pickle=True)

def queueClear():
    # used to clear the list
    save.list_finalResult = []
    np.save(savePath, save.list_finalResult, allow_pickle=True)

if __name__ == '__main__':
    queueClear()