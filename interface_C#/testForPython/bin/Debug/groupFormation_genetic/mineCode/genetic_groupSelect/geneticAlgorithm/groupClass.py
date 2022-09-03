"""
The list of the group, used to calculate the info of one group, the homo and heter value.
"""
from mineCode import save
import copy
from sklearn.preprocessing import MinMaxScaler
import numpy as np

class Group:
    def __init__(self, list_students):
        self.list_students = list_students
        self.list_studentObjects = self.studentObjectFind()

        # the value that need to be calculated
        # the part that could be calculated directly
        self.homoDistance, self.heterDistance = self.distanceCalculated()

        # the values that need to be calculated later
        self.homoValue, self.heterValue = self.valueCalculated()
        # calcualte the final value
        self.finalValue = self.homoValue + self.heterValue

    def studentObjectFind(self):
        """
        For each student position, find the corresponding Student object from the list in save.
        :return: the object list for this group
        """
        # the list to save
        list_stuObjects = []
        # from the save, get all the students' objects
        for stuPosition in self.list_students:
            stuObject = save.list_student[stuPosition]
            list_stuObjects.append(stuObject)
        return list_stuObjects


    def distanceCalculated(self):
        """
        Used to calculate the distance inside the group.
        1. for the [type] kind of data, using the unrepeated number to determine the distance.
        2. for other kind of data, using maha distance from the save py to determine the distance.
        3. combine the dimension distance, by homo/heter.
        :return: the average of homo and heter distance
        """
        group = self.list_students
        # create the sum to save
        homoDistanceSum = 0
        heterDistanceSum = 0

        # loop to find the member pairs, calculate the [vectors] kind of data
        for i in range(len(group)):
            for j in range(i + 1, len(group)):
                groupId01 = group[i]
                groupId02 = group[j]
                homoDistanceSum += save.dict_mahaDistance['homo'][groupId01][groupId02]
                heterDistanceSum += save.dict_mahaDistance['heter'][groupId01][groupId02]

        # loop to calculate the [type] kind of data
        sample = self.list_studentObjects[0]
        # one dimension at a time
        for dimension in sample.dict_type.keys():
            # used to save the members' info for this exact dimension
            dimensionList = []
            for student in self.list_studentObjects:
                # get the info from save
                thisValue = student.dict_type[dimension]
                dimensionList.append(thisValue)

            # using the set to find the unrepeated tags, and then count the distance
            # the more unrepeated there is, the longer the distance is
            reducedList = list(set(dimensionList))
            unrepeatNum = len(reducedList)
            dimensionDistance = unrepeatNum * save.fixValue_type

            # judge, where the dimension belongs
            if dimension in sample.list_homoType:
                homoDistanceSum += dimensionDistance
            else:
                heterDistanceSum += dimensionDistance

        homoDistanceAverage = homoDistanceSum / len(self.list_students)
        heterDistanceAverage = heterDistanceSum / len(self.list_students)

        return [homoDistanceAverage, heterDistanceAverage]

    def valueCalculated(self):
        # put the max and min into the list
        homoDList = copy.deepcopy(save.dict_mahaBorderValue['homo'])
        homoDList.append(self.homoDistance)
        heterDList = copy.deepcopy(save.dict_mahaBorderValue['heter'])
        heterDList.append(self.heterDistance)

        # normalize the homo and heterValue
        # initial the scaler, the range
        scaler_homo = MinMaxScaler(feature_range=(-1, 0))
        scaler_heter = MinMaxScaler(feature_range=(0, 1))
        # get the list into sample arrangement [in the col]
        homoArray = np.array(homoDList).reshape(-1, 1)
        heterArray = np.array(heterDList).reshape(-1, 1)

        homoValueList = list(scaler_homo.fit_transform(homoArray))
        heterValueList = list(scaler_heter.fit_transform(heterArray))

        # the list should be, [min, max, thisValue], thus the [2] is the target value
        return [-homoValueList[2], heterValueList[2]]

    def __str__(self):
        # in case something wrong with str
        from builtins import str

        # the list of the string
        strList = []

        # the students' list
        str_stuList = 'Group members: ' + str(self.list_students)
        strList.append(str_stuList)

        # the distances
        str_distance = "Distances: "
        # homoDistance
        str_distance += '\n  Homo Distance: ' + str(self.homoDistance)
        # heterDistance
        str_distance += '\n  Heter Distance: ' + str(self.heterDistance)
        strList.append(str_distance)

        # the values
        str_value = "Values: "
        # homoValue
        str_value += '\n  Homo Value: ' + str(self.homoValue)
        # heterValue
        str_value += '\n  Heter Value: ' + str(self.heterValue)
        # finalValue
        str_value += '\n  Whole Value: ' + str(self.finalValue)
        strList.append(str_value)

        # finalStr
        wholeStr = ''
        for str in strList:
            wholeStr += str + '\n'

        return wholeStr
