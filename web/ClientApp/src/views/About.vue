<template>
    <div>
        <div class="flex two">
            <!--左半-->
            <div>
                <!--填寫 Json區塊-->
                <div>
                    <textarea v-model="jsonBox" cols="70" rows="10"></textarea>
                </div>

                <!--<radio></radio>-->
                <button @click="genReport(jsonBox)">產生問卷</button>
                <button @click="genJson()">獲取Json</button>
                <!--自訂選擇區塊-->
                <createForm :data="this.data"></createForm>
            </div>
            <!--右半 顯示問卷-->
            <div class="">
                <!--產生問卷-->
                <!--// data 物件內容描述-->
                <!--// Property-->
                <!--// id: 該題組ID,-->
                <!--// type: 類型 例如'checkBox', 'jsonBox'-->
                <!--// displayName: 顯示名稱,-->
                <!--// value: 值,-->
                <!--// belong: 值,-->
                <!--// show: 是否顯示 e.g. true, false,-->
                <component v-for="(item,index) in data" :key="data.displayName"
                           v-show="item.show"
                           v-bind:is="item.type"
                           :data="item"
                           @onChange="updateForm"
                ></component>
            </div>
        </div>
    </div>
</template>


<script>
    import checkBox from '../components/recordType/checkBox.vue';
    import question from '../components/recordType/question.vue';
    import textBox from '../components/recordType/textBox.vue';
    import radio from '../components/recordType/radio';
    import createForm from '../components/createComponent/createForm.vue';

    export default {
        data() {
            return {
                jsonBox: '',
                // jsonBox: '[{"id":"1","type":"question","displayName":"你抽菸嗎","value":""},{"id":"2","type":"checkBox","displayName":"抽都抽","value":""},{"id":"3","type":"checkBox","displayName":"哥不抽","value":""},\n' +
                //          '{"id":"4","type":"question","displayName":"一天抽幾次","value":"","belong":"2"},\n' +
                //          '{"id":"5","type":"checkBox","displayName":"10次","value":"","belong":"2"},{"id":"6","type":"checkBox","displayName":"100次","value":"","belong":"2"}]',
                data: [],
            };
        },
        components: {
            'checkBox': checkBox,
            'question': question,
            'textBox': textBox,
            'createForm': createForm,
            radio,
        },
        created() {
            console.log(JSON.stringify(this.data));
        },
        methods: {
            // 顯示問卷的Json
            genJson: function () {
                const tmp = JSON.parse(JSON.stringify(this.data));
                tmp.map(item => delete item.show);
                this.jsonBox = JSON.stringify(tmp);
            },
            // 用Json　產生問卷
            genReport: function (JsonString) {
                // 試著將Json轉成Object
                let result = this.parseToObject(JsonString);
                //成功的話
                if (result) {
                    this.processBelongTo(result);
                }
            },
            // 試著將Json轉成Object 失敗 return false
            parseToObject: function (jsonString) {
                try {
                    return JSON.parse(jsonString);
                } catch (err) {
                    return false;
                }
            },
            // 處理跳題的顯示問題後，丟進Component中顯示
            processBelongTo: function (data) {
                data.map(function (element, index, array) {
                    if (element.belong) {
                        const sta = array.find((a) => a.id === element.belong);
                        if (sta) {
                            element.show = (sta.value !== '');
                        }
                        else
                            element.show = false;
                    } else {
                        element.show = true;
                    }
                });
                console.log('處理後的data',data);
                this.data = data;
            },
            updateForm: function (id, value) {
                this.data.map(function (element) {
                    if (element.belong === id) {
                        //console.log('update form',value);
                        element.show = value;
                    }
                });
            },
        },
    };
</script>

<style scoped>
    .rightSide {
        margin: 10px 70px;

    }
</style>
