const convertKeysToLowerCase = (data) => {
        const newData = {};
        for (let key in data) {
            if (data.hasOwnProperty(key)) {
                newData[key.toLowerCase()] = data[key];
            }
        }
        return newData;
    };

    const fetchZvireDetails = async (id) => {
        try {
            const response = await fetch(`http://localhost:5240/api/Zvirata/${id}`);
            if (!response.ok) {
                console.error('Chyba při načítání dat zvířat:', response.statusText);
                return;
            }
            const data = await response.json();
            const lowerCaseData = convertKeysToLowerCase(data);  // Převod klíčů na malá písmena
            console.log(lowerCaseData);  // Zkontrolujte, co API vrací
            updateDetailPanel(lowerCaseData);
        } catch (error) {
            console.error('Chyba při volání API:', error);
        }
    };

    const tableRows = document.querySelectorAll('.table-list-body-row');

    tableRows.forEach(row => {
        row.addEventListener('click', () => {
            const id = row.dataset.id; // Pokud je id kliniky uloženo v datatributu
            fetchZvireDetails(id); // Zavolání API pro načtení detailu kliniky
        });
    });

    const updateDetailPanel = (data) => {
        document.getElementById('detail-name').value = data.jméno_zvířete || '';
        document.getElementById('detail-species').value = data.druh || '';
        document.getElementById('detail-age').value = data.věk || ''; 
        document.getElementById('detail-health').value = data.zdravotní_stav
            || ''; 
        document.getElementById('detail-weight').value = data.váha
            || ''; 
        document.getElementById('detail-ownername').value = data.jméno_majitele
 || ''; 
        document.getElementById('detail-ownersurname').value = data.příjmení_majitele
 || ''; 
        document.getElementById('detail-phone').value = data.telefonní_číslo
 || ''; 
        document.getElementById('detail-address').value = data.adresa_majitele || ''; 
        document.getElementById('detail-diagnosis').value = data.název_diagnózy || 'Nemá'; 
        document.getElementById('detail-therapy').value = data.popís_lečby
|| 'Nemá'; 
        
    };