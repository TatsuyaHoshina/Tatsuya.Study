/// <reference path="hubot.d.ts" />

import hubot = require("hubot");
// import https = require("https");
// import querystring = require("querystring");

// let httpsRequest = https.request({
//   host: "api.cognitive.microsoft.com",
//   path: "/sts/v1.0/issueToken",
//   method: "POST",
//   headers: {
//     "Ocp-Apim-Subscription-Key": "キー"
//   }
// }, function (result) {
//   // result.on("data"〜 でがんばる
// });
// httpsRequest.write(querystring.stringify({}));  // 多分必要
// httpsRequest.end();

module.exports = (robot: hubot.Robot): void => {
  robot.respond(/is it (xmas|christmas)\s?\?/i, (msg: hubot.Response) => {
    var today: Date = new Date();

    msg.reply(today.getDate() === 25 &&
      (today.getMonth() + 1) === 12 ? "YES" : "NO");
  });
};
