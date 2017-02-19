/// <reference path="hubot.d.ts" />
"use strict";
var https = require("https");
var querystring = require("querystring");
var httpsRequest = https.request({
    host: "api.cognitive.microsoft.com",
    path: "/sts/v1.0/issueToken",
    method: "POST",
    headers: {
        "Ocp-Apim-Subscription-Key": "キー"
    }
}, function (result) {
    // result.on("data"〜 でがんばる
});
httpsRequest.write(querystring.stringify({})); // 多分必要
httpsRequest.end();
module.exports = function (robot) {
    robot.respond(/is it (xmas|christmas)\s?\?/i, function (msg) {
        var today = new Date();
        msg.reply(today.getDate() === 25 &&
            (today.getMonth() + 1) === 12 ? "YES" : "NO");
    });
};
