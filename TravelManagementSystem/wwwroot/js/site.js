//const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');

//allSideMenu.forEach(item => {
//	const li = item.parentElement;

//	item.addEventListener('click', function () {
//		allSideMenu.forEach(i => {
//			i.parentElement.classList.remove('active');
//		})
//		li.classList.add('active');
//	})


//});
const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');

// Function to set active menu item based on current URL
function setActiveMenu() {
	const currentPath = window.location.pathname; // Get the current path

	allSideMenu.forEach(item => {
		const li = item.parentElement;
		li.classList.remove('active'); // Remove active class from all items

		if (item.getAttribute('href') === currentPath) {
			li.classList.add('active'); // Add active class to the matching item
		}
	});
}

// Call the function on page load
setActiveMenu();

// Add click event listeners (if needed for custom behavior)
allSideMenu.forEach(item => {
	item.addEventListener('click', function (e) {
		allSideMenu.forEach(i => {
			i.parentElement.classList.remove('active'); // Remove active class from all items
		});
		item.parentElement.classList.add('active'); // Add active class to the clicked item
	});
});




// TOGGLE SIDEBAR
const menuBar = document.querySelector('#content nav .bx.bx-menu');
const sidebar = document.getElementById('sidebar');

menuBar.addEventListener('click', function () {
	sidebar.classList.toggle('hide');
})







const searchButton = document.querySelector('#content nav form .form-input button');
const searchButtonIcon = document.querySelector('#content nav form .form-input button .bx');
const searchForm = document.querySelector('#content nav form');

searchButton.addEventListener('click', function (e) {
	if (window.innerWidth < 576) {
		e.preventDefault();
		searchForm.classList.toggle('show');
		if (searchForm.classList.contains('show')) {
			searchButtonIcon.classList.replace('bx-search', 'bx-x');
		} else {
			searchButtonIcon.classList.replace('bx-x', 'bx-search');
		}
	}
})





if (window.innerWidth < 768) {
	sidebar.classList.add('hide');
} else if (window.innerWidth > 576) {
	searchButtonIcon.classList.replace('bx-x', 'bx-search');
	searchForm.classList.remove('show');
}


window.addEventListener('resize', function () {
	if (this.innerWidth > 576) {
		searchButtonIcon.classList.replace('bx-x', 'bx-search');
		searchForm.classList.remove('show');
	}
})



const switchMode = document.getElementById('switch-mode');

switchMode.addEventListener('change', function () {
	if (this.checked) {
		document.body.classList.add('dark');
	} else {
		document.body.classList.remove('dark');
	}
})