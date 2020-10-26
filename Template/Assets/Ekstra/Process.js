/*
 *  Process  : "Add", "Edit", "Update", "Delete"  
 *  modelName : "Büyük harfle başlar, sunucu tarafındaki controllerda kullanılan isimle aynı olmalıdır."
 *  obj : "Sunucu tarafında bulunan modelle aynı özelliklere sahip olmalıdır."
 *  cb : "İşlem tamamlandıktan veya hatayla karşılaştıktan sonra gönderilen fonksiyonu çalıştırır."
*/

var MeetApp = (function (m) {
    m.Process = {
        GetUrl: function (process, modelName) { return MeetApp.Server.Url + process + modelName },
        Run: function (process, modelName, obj, cb) {
            $.ajax({
                type: 'POST',
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                url: MeetApp.Process.GetUrl(process, modelName)
            }).done(function (data) {
                cb(data);
            });
        }
    }
    return m;
}(MeetApp || {}))