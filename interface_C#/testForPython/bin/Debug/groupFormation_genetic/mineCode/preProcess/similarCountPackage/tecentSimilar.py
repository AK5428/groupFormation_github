import json
from tencentcloud.common import credential
from tencentcloud.common.profile.client_profile import ClientProfile
from tencentcloud.common.profile.http_profile import HttpProfile
from tencentcloud.common.exception.tencent_cloud_sdk_exception import TencentCloudSDKException
from tencentcloud.nlp.v20190408 import nlp_client, models
import mineCode.save as save

# result model
# {"Similarity": [{"Text": "math Study", "Score": 0.9325706663930653}, {"Text": "Asian Economics", "Score": 0.6833215077912473}], "RequestId": "0645c9f4-5392-4d67-a1ff-5a7a45e2c853"}

def similarityCalculate(input, matchList):
    resultDict = {'Text': 'none', 'Score': 0}

    try:
        cred = credential.Credential(save.SecretId, save.SecretKey)
        httpProfile = HttpProfile()
        httpProfile.endpoint = "nlp.tencentcloudapi.com"

        clientProfile = ClientProfile()
        clientProfile.httpProfile = httpProfile
        client = nlp_client.NlpClient(cred, "ap-guangzhou", clientProfile)

        req = models.TextSimilarityRequest()
        params = {
            "SrcText": input,
            "TargetText": matchList
        }
        req.from_json_string(json.dumps(params))

        resp = client.TextSimilarity(req)
        # print(resp.to_json_string())

        # added by myself
        dict = json.loads(resp.to_json_string())

        return dict['Similarity']

    except TencentCloudSDKException as err:
        print(err)



if __name__ == '__main__':
    matchList = ["math Study", "Asian Economics"]
    # the list of result
    list = similarityCalculate('math', matchList)
    print(list)
