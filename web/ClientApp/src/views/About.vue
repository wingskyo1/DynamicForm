<template>
    <div>
        <div class="flex two">
            <!--左半-->
            <div>
                <!--填寫 Json區塊-->
                <div>
                    <textarea v-model="textBox" cols="70" rows="10"></textarea>
                </div>
                <button @click="genReport(textBox)">產生問卷</button>
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
                <!--// type: 類型 例如'checkBox', 'textBox'-->
                <!--// displayName: 顯示名稱,-->
                <!--// value: 值,-->
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
    import createForm from '../components/createComponent/createForm.vue';

    export default {
        data() {
            return {
                textBox: '',
                // textBox: '[{"id":"1","type":"question","displayName":"你抽菸嗎","value":""},{"id":"2","type":"checkBox","displayName":"抽都抽","value":""},{"id":"3","type":"checkBox","displayName":"哥不抽","value":""},\n' +
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
        },
        created() {
            console.log(JSON.stringify(this.data));
        },
        methods: {
            genJson: function () {
                const tmp = JSON.parse(JSON.stringify(this.data));
                tmp.map(item => delete item.show);
                this.textBox = JSON.stringify(tmp);
            },
            genReport: function (JsonString) {
                // parse to JSON Object
                let result = this.parseToObject(JsonString);
                // if no err then set belong to
                if (result) {
                    this.processBelongTo(result);
                }
            },
            parseToObject: function (jsonString) {
                try {
                    return JSON.parse(jsonString);
                } catch (err) {
                    return false;
                }
            },
            processBelongTo: function (data) {
                data.map(function (element, index, array) {
                    if (element.belong) {
                        const sta = array.find((a) => a.id === element.belong);
                        element.show = (sta.value === true);
                    } else {
                        element.show = true;
                    }
                });
                this.data = data;
            },
            updateForm: function (id, value) {
                this.data.map(function (element) {
                    if (element.belong === id) {
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
