"""
The colony class, used to save all the students object, and store the similarities as well.
1. store the similarities in the middle steps.
2. save the students' objects.
"""

class Colony:
    def __init__(self, list_students, name):
        # the info of all the students
        self.name = name
        self.list_students = list_students

        # the boolean to detect if the tag similarities need to be calculated
        self.hasTagSimilar, self.hasTagMix = self.tagTypeJudge()

        # the dict used to save all the similarities corresponding to the students' info
        self.dict_tagSimilarities = {}
        self.dict_tagPair = {}

    def tagTypeJudge(self):
        # used to save the boolean
        booleanList = []

        # get the first student as sample
        sample = self.list_students[0]

        # if the sample has tag_similar
        booleanList.append(True) if not sample.dict_tagSimilar == {} else booleanList.append(False)
        booleanList.append(True) if not sample.dict_tagMix == {} else booleanList.append(False)

        return booleanList

