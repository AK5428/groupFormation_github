"""
Used to calculate the mahaDistance, using the homo and heter vectors.
1. combine the vectors into array.
2. using maha method to calculate the exact distance between the students, separately in homo/heter
"""
from mineCode import save
import numpy as np
from sklearn import decomposition


def mahaDistanceStart():
    """
    Used to combine the homo and heter vector from all the persons.
    And call the mahaDistanceFunc to calculate them.
    :return: none
    """
    # build the list for homo and heter, combine all the member into one
    homoAllList = []
    heterAllList = []
    for studnet in save.list_student:
        homoAllList.append(studnet.homoVector)
        heterAllList.append(studnet.heterVector)

    # separately calculate the homo and heter vectors in mahaDistance
    mahaForSingleDirection('homo', homoAllList)
    mahaForSingleDirection('heter', heterAllList)
    return

def mahaForSingleDirection(dimensionName, vectorList):
    """
    Used to calculate the mahaDistance for homo or heter.
    :param dimensionName: homo or heter, should be store into different place.
    :param vectorList: the combined list of homo or heter
    :return: none
    """
    print(vectorList)
    """See if the values for this specific dimension is less than 1, if so"""
    # if the choices goes that, only 1 or 0 number appears for heter or homo
    # if there's nothing for this dimension, make all the distance 0, then return
    if(len(vectorList[0]) == 0):
        # the num of students
        stuNum = len(vectorList)
        # create a two dimension's list to store the info
        distanceList = [[0 for i in range(0, stuNum)] for j in range(0, stuNum)]
        # save it in the save py
        save.dict_mahaDistance.update({dimensionName: distanceList})
        return
    # if there's only 1 value for this dimension
    elif(len(vectorList[0]) == 1):
        # the num of students
        stuNum = len(vectorList)
        # create a two dimension's list to store the info
        distanceList = [[0 for i in range(0, stuNum)] for j in range(0, stuNum)]
        for i in range(0, stuNum):
            for j in range(i + 1, stuNum):
                distance = abs(vectorList[i][0] - vectorList[j][0])
                distanceList[i][j] = distance
                distanceList[j][i] = distance
        # save the distance, then return
        save.dict_mahaDistance.update({dimensionName: distanceList})
        return

    """If the values are more then 2"""
    # single dimension means homo or heter
    # get the vectors
    vector = np.vstack(vectorList)
    vectorT = vector.T
    thisCov = np.cov(vectorT)

    # in case some samples are same
    # if unable to reverse, do PCA
    if np.linalg.det(thisCov) == 0.0:
        print("cov = 0")
        pcaVectorArray = np.array(vectorList)
        pca = decomposition.PCA(n_components='mle')
        pca.fit(pcaVectorArray)
        vectorT = pcaVectorArray.T

        # find the new cov and then calculate the reverseCov
        newCov = np.cov(vectorT)
        reverseCov = np.linalg.inv(newCov)

    # else, get the reverseCov directly
    else:
        reverseCov = np.linalg.inv(thisCov)


    # the num of students
    stuNum = len(vectorList)
    # create a two dimension's list to store the info
    distanceList = [[0 for i in range(0, stuNum)] for j in range(0, stuNum)]
    for i in range(0, stuNum):
        for j in range(i + 1, stuNum):
            delta = vector[i] - vector[j]
            distance = np.sqrt(np.dot(np.dot(delta, reverseCov), delta.T))
            distanceList[i][j] = distance
            distanceList[j][i] = distance

    # save it in the save py
    save.dict_mahaDistance.update({dimensionName: distanceList})
