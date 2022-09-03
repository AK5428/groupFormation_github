"""
The queue class, contains all the group message
"""
import numpy as np

from mineCode import save
from mineCode.genetic_groupSelect.geneticAlgorithm.groupClass import Group

class Queue:
    def __init__(self, groupList):
        self.groupList = groupList

        # the list used to store the user's choice
        self.homoChoiceList = save.homoDimension
        self.heterChoiceList = save.heterDimension

        # build the groups
        self.list_groupObject = self.groupListBuild(groupList)
        # calculate and update the values
        self.queueVariance = -1
        self.queueAverage = -1
        self.finalValue = -1
        self.queueEvaluate()

        # calculate and update the average distance separately for homo and heter
        self.homoDAverage, self.heterDAverage = self.averageDCalculate()

        # for the genetic algorithm, calculate the proportion
        self.proportion = -1


    def averageDCalculate(self):
        """
        Used to calculate the average distance separately for homo and heter.
        :return: the distances.
        """
        # get the sum of the distance
        homoSum, heterSum = [0, 0]

        # calculate the sum
        for group in self.list_groupObject:
            homoSum += group.homoDistance
            heterSum += group.heterDistance

        # calculate the average and return it
        homoDAverage = homoSum / len(self.list_groupObject)
        heterDAverage = heterSum / len(self.list_groupObject)
        return [homoDAverage, heterDAverage]

    def queueEvaluate(self):
        """
        Used to calculate the average, variance and final value.
        :return: none
        """

        valueList = []
        for group in self.list_groupObject:
            valueList.append(group.finalValue)

        self.bestGroup = self.list_groupObject[valueList.index(np.max(valueList))]

        # calculate the variance and average
        self.queueVariance = np.var(valueList)
        self.queueAverage = np.average(valueList)

        # the final value
        if save.isEquilibrium:
            self.finalValue = self.queueAverage + save.varianceDivisor / self.queueVariance
        else:
            self.finalValue = self.queueAverage


    def groupListBuild(self, groupList):
        """
        For the groups in the queue, build the Group object.
        :param groupList: contains the numbers of the group
        :return: the object list
        """
        # for each group, build the object for them and then save in list
        groupObjectList = []
        for group in groupList:
            newGroup = Group(group)
            groupObjectList.append(newGroup)
        return groupObjectList

    def __str__(self):
        """
        Used to print Queue objects.
        :return:
        """
        # in case something wrong with str
        from builtins import str

        # the list of the string
        strList = []

        # str_idNum = 'Queue id: ' + str(self.idNum)
        str_bestGroup = 'Best group: ' + str(self.bestGroup)
        strList.extend([str_bestGroup])
        str_groupList = 'Groups: ' + str(self.groupList)
        strList.extend([str_groupList])

        # the queue values
        str_distance = "Average distances: "
        # homo
        str_distance += '\n  Homo Average Distance: ' + str(self.homoDAverage)
        # variance
        str_distance += '\n  Heter Average Distance: ' + str(self.heterDAverage)
        strList.append(str_distance)

        # the queue values
        str_values = "Values: "
        # average
        str_values += '\n  Queue Average: ' + str(self.queueAverage)
        # variance
        str_values += '\n  Queue Variance: ' + str(self.queueVariance)
        # final value
        str_values += '\n  Final Value: ' + str(self.finalValue)
        strList.append(str_values)

        # finalStr
        wholeStr = ''
        for str in strList:
            wholeStr += str + '\n'

        return wholeStr
