var deleteModal = document.getElementById('deleteModal');
deleteModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    var gearId = button.getAttribute('data-gear-id');
    var gearName = button.getAttribute('data-gear-name');

    var modalGearName = deleteModal.querySelector('#gearName');
    modalGearName.textContent = gearName;

    var form = deleteModal.querySelector('#deleteForm');
    form.action = '/SportGear/Delete/' + gearId;
});