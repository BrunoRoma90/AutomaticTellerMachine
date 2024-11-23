function insertNumber(num) {
    var number = document.getElementById('screen').innerHTML
    document.getElementById('screen').innerHTML = number + num;
}

function clearScreen() {
    const choice = document.getElementById('screen')
    choice.innerHTML = ""

}

function backspace() {
    const choice = document.getElementById('screen')
    choice.innerHTML = choice.innerHTML.substring(0, choice.innerHTML.length - 1)
}

function confirm() {
    const choice = document.getElementById('screen')
    choice.innerHTML = ""

}

function atmSubmitForm() {
    document.getElementById('screenForm').submit()
}