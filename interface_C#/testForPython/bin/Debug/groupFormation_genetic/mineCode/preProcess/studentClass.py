"""
The py of student class, used to save all the data for a single students.
Separated with dimension.
"""

class Student:
    typeDict = {}

    def __init__(self, stuNum, stuId, stuName):
        # the original data
        self.stuNum = stuNum
        self.stuId = stuId
        self.stuName = stuName
        self.dict_tagMix = {}
        self.dict_tagSimilar = {}
        self.dict_tagWeight = {}

        # the final vector. the tag type have to be processed first.
        # others can be used directly, as the original.
        self.dict_tag = {}
        self.dict_value = {}
        self.dict_type = {}
        self.dict_network = {}

        # the vector of final
        self.homoVector = []
        self.heterVector = []
        self.list_homoType = []
        self.list_heterType = []


    def __str__(self):
        # in case something wrong with str
        from builtins import str

        # the list of the string
        strList = []

        # the num and id
        str_stuNum = 'stuNum: ' + str(self.stuNum)
        str_stuId = 'stuId: ' + str(self.stuId)
        str_stuName = 'stuName: ' + str(self.stuName)
        strList.extend([str_stuNum, str_stuId, str_stuName])

        # the dict_tagMix
        if not self.dict_tagMix == {}:
            str_tagMix = 'Tag_mix:'
            for key in self.dict_tagMix:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_tagMix[key])
                str_tagMix += '\n' + dimensionStr
            strList.append(str_tagMix)

        # the dict_tagSimilar
        if not self.dict_tagSimilar == {}:
            str_tagSimilar = 'Tag_similar:'
            for key in self.dict_tagSimilar:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_tagSimilar[key])
                str_tagSimilar += '\n' + dimensionStr
            strList.append(str_tagSimilar)

        # the dict_tagWeight
        if not self.dict_tagWeight == {}:
            str_tagWeight = 'Tag_weight:'
            for key in self.dict_tagWeight:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_tagWeight[key])
                str_tagWeight += '\n' + dimensionStr
            strList.append(str_tagWeight)

        # the dict_value
        if not self.dict_value == {}:
            str_value = 'Value:'
            for key in self.dict_value:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_value[key])
                str_value += '\n' + dimensionStr
            strList.append(str_value)

        # the dict_value
        if not self.dict_type == {}:
            str_type = 'Type:'
            for key in self.dict_type:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_type[key])
                str_type += '\n' + dimensionStr
            strList.append(str_type)

        # the dict_network
        if not self.dict_network == {}:
            str_network = 'Network:'
            for key in self.dict_network:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_network[key])
                str_network += '\n' + dimensionStr
            strList.append(str_network)

        # the dict_tag
        # with all similarities
        if not self.dict_tag == {}:
            str_tag = 'Tag:'
            for key in self.dict_tag:
                dimensionStr = '   ' + key + ' : ' + str(self.dict_tag[key])
                str_tag += '\n' + dimensionStr
            strList.append(str_tag)

        # finalStr
        wholeStr = ''
        for str in strList:
            wholeStr += str + '\n'

        return wholeStr


