<template>
    <div>
        <div class="flex four">
            <div class="three-fourth">
                <input v-model="belongTo" placeholder="綁定跳題"/>
            </div>
        </div>


        <div class="flex four">
            <div class="half">
                <input v-model="createQuestionName" placeholder="題目名稱"/>
            </div>
            <div>
                <select v-model="selectedType">
                    <option>Type</option>
                    <option value="radio">Radio</option>
                    <option value="checkBox">CheckBox</option>
                    <option value="textBox">TextBox</option>
                </select>
            </div>

            <div>
                <button class='success' @click="newRecord">Add</button>
            </div>
        </div>

        <div>
            <createInput v-for="item in createRecord" :key="createRecord.id" @delRecord="delRecord"
                         :data="item"></createInput>
        </div>
        <button v-if="createRecord.length>0" class='warning' @click="finishQuestion">送出</button>

    </div>
</template>

<script>

    import createInput from '../../components/createComponent/createInput.vue';

    export default {
        props: {
            data: {
                type: Array,
            },
        },
        data() {
            return {
                // CreateRecord結構
                //  id
                //  name
                createRecord: [],
                selectedType: '',
                createQuestionName: '',
                belongTo: '',
            };
        },
        components: {
            'createInput': createInput,
        },
        methods: {
            newRecord: function () {
                const object = {id: this.IDGenerator(), name: ''};
                this.createRecord.push(object);
            },
            // 填寫完問題產生問題樣板
            finishQuestion: function () {
                this.fillQuestion();
                switch (this.selectedType) {
                    case 'checkBox':
                        this.fillCheckBox();
                        break;
                    case 'radio':
                        this.fillRadio();
                        break;
                    case 'textBox':
                        this.fillTextBox();
                        break;
                };
                console.log('顯示產生的問結構',this.data);
                this.resetForm();
            },
            // 填入問題
            fillQuestion: function () {
                const object = {
                    id: this.IDGenerator(),
                    type: 'question',
                    displayName: this.createQuestionName,
                    value: '',
                    belong: this.belongTo,
                    show: true,
                };
                this.data.push(object);
            },
            // CheckBox = selectedType
            fillCheckBox: function () {
                this.createRecord.map((item, index) => {
                    const object = {
                        id: item.id,
                        type: this.selectedType,
                        displayName: item.name,
                        value: '',
                        belong: this.belongTo,
                        show: true,
                    };
                    this.data.push(object);
                });
            },
            // Radio  = selectedType
            fillRadio: function () {
                const object = {
                    id: this.IDGenerator(),
                    type: 'radio',
                    displayName: this.createRecord,
                    value: '',
                    belong: this.belongTo,
                    show: true,
                };
                this.data.push(object);
            },
            // TextBox = selectedType
            fillTextBox: function () {
                const object = {
                    id: this.IDGenerator(),
                    type: 'textBox',
                    displayName:'',
                    value: '',
                    belong: this.belongTo,
                    show: true,
                };
                this.data.push(object);
            },
            // 刪除答案
            delRecord: function (id) {
                const index = this.createRecord.findIndex(obj => obj.id === id);
                this.createRecord.splice(index, 1);
            },
            // ID產生器
            IDGenerator: function () {
                // always start with a letter (for DOM friendlyness)
                let idStr = String.fromCharCode(Math.floor((Math.random() * 25) + 65));
                do {
                    // between numbers and characters (48 is 0 and 90 is Z (42-48 = 90)
                    const ascicode = Math.floor((Math.random() * 42) + 48);
                    if (ascicode < 58 || ascicode > 64) {
                        // exclude all chars between : (58) and @ (64)
                        idStr += String.fromCharCode(ascicode);
                    }
                } while (idStr.length < 32);
                return (idStr);
            },
            // 新增完一個問題　Reset資料
            resetForm: function () {
                this.createQuestionName = '';
                this.createRecord = [];
                this.selectedType = '';
                this.belongTo = '';
            },
        },
    };

</script>
