# -*- coding: utf8 -*-
# Copyright (c) 2017-2021 THL A29 Limited, a Tencent company. All Rights Reserved.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#    http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

import json

from tencentcloud.common.exception.tencent_cloud_sdk_exception import TencentCloudSDKException
from tencentcloud.common.abstract_client import AbstractClient
from tencentcloud.market.v20191010 import models


class MarketClient(AbstractClient):
    _apiVersion = '2019-10-10'
    _endpoint = 'market.tencentcloudapi.com'
    _service = 'market'


    def FlowProductRemind(self, request):
        """计量商品用量提醒，用于服务商调用云服务，云服务向客户发送提醒信息

        :param request: Request instance for FlowProductRemind.
        :type request: :class:`tencentcloud.market.v20191010.models.FlowProductRemindRequest`
        :rtype: :class:`tencentcloud.market.v20191010.models.FlowProductRemindResponse`

        """
        try:
            params = request._serialize()
            body = self.call("FlowProductRemind", params)
            response = json.loads(body)
            if "Error" not in response["Response"]:
                model = models.FlowProductRemindResponse()
                model._deserialize(response["Response"])
                return model
            else:
                code = response["Response"]["Error"]["Code"]
                message = response["Response"]["Error"]["Message"]
                reqid = response["Response"]["RequestId"]
                raise TencentCloudSDKException(code, message, reqid)
        except Exception as e:
            if isinstance(e, TencentCloudSDKException):
                raise
            else:
                raise TencentCloudSDKException(e.message, e.message)


    def GetUsagePlanUsageAmount(self, request):
        """该接口可以根据InstanceId查询实例的api的使用情况。

        :param request: Request instance for GetUsagePlanUsageAmount.
        :type request: :class:`tencentcloud.market.v20191010.models.GetUsagePlanUsageAmountRequest`
        :rtype: :class:`tencentcloud.market.v20191010.models.GetUsagePlanUsageAmountResponse`

        """
        try:
            params = request._serialize()
            body = self.call("GetUsagePlanUsageAmount", params)
            response = json.loads(body)
            if "Error" not in response["Response"]:
                model = models.GetUsagePlanUsageAmountResponse()
                model._deserialize(response["Response"])
                return model
            else:
                code = response["Response"]["Error"]["Code"]
                message = response["Response"]["Error"]["Message"]
                reqid = response["Response"]["RequestId"]
                raise TencentCloudSDKException(code, message, reqid)
        except Exception as e:
            if isinstance(e, TencentCloudSDKException):
                raise
            else:
                raise TencentCloudSDKException(e.message, e.message)