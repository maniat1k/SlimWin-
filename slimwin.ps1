# Cargar ensamblado para formularios
Add-Type -AssemblyName System.Windows.Forms

# Crear ventana principal
$form = New-Object System.Windows.Forms.Form
$form.Text = "Debloat Windows 11"
$form.Size = New-Object System.Drawing.Size(400, 500)
$form.StartPosition = "CenterScreen"

# Etiqueta de instrucciones
$label = New-Object System.Windows.Forms.Label
$label.Text = "Selecciona las opciones que deseas aplicar:"
$label.Size = New-Object System.Drawing.Size(350, 20)
$label.Location = New-Object System.Drawing.Point(20, 20)
$form.Controls.Add($label)

# Variables de control para el botón de cancelar
$cancelRequested = $false

# Botón de cancelar
$buttonCancel = New-Object System.Windows.Forms.Button
$buttonCancel.Text = "Cancelar"
$buttonCancel.Location = New-Object System.Drawing.Point(270, 400)
$buttonCancel.Size = New-Object System.Drawing.Size(100, 30)
$form.Controls.Add($buttonCancel)

# Función para manejar la cancelación
$buttonCancel.Add_Click({
    $cancelRequested = $true
    [System.Windows.Forms.MessageBox]::Show("Proceso cancelado. Saldrá al terminar la tarea en curso.", "Cancelado")
})

# Checkbox para cada acción
$checkboxBackup = New-Object System.Windows.Forms.CheckBox
$checkboxBackup.Text = "Crear copia de seguridad"
$checkboxBackup.Location = New-Object System.Drawing.Point(20, 60)
$checkboxBackup.Checked = $true
$form.Controls.Add($checkboxBackup)

$checkboxBloatware = New-Object System.Windows.Forms.CheckBox
$checkboxBloatware.Text = "Eliminar aplicaciones preinstaladas"
$checkboxBloatware.Location = New-Object System.Drawing.Point(20, 100)
$checkboxBloatware.Checked = $true
$form.Controls.Add($checkboxBloatware)

$checkboxServices = New-Object System.Windows.Forms.CheckBox
$checkboxServices.Text = "Deshabilitar servicios innecesarios"
$checkboxServices.Location = New-Object System.Drawing.Point(20, 140)
$checkboxServices.Checked = $true
$form.Controls.Add($checkboxServices)

$checkboxPrivacy = New-Object System.Windows.Forms.CheckBox
$checkboxPrivacy.Text = "Configurar ajustes de privacidad"
$checkboxPrivacy.Location = New-Object System.Drawing.Point(20, 180)
$checkboxPrivacy.Checked = $true
$form.Controls.Add($checkboxPrivacy)

$checkboxClean = New-Object System.Windows.Forms.CheckBox
$checkboxClean.Text = "Liberar espacio en disco"
$checkboxClean.Location = New-Object System.Drawing.Point(20, 220)
$checkboxClean.Checked = $true
$form.Controls.Add($checkboxClean)

# Botón de ejecutar
$buttonExecute = New-Object System.Windows.Forms.Button
$buttonExecute.Text = "Ejecutar"
$buttonExecute.Location = New-Object System.Drawing.Point(20, 400)
$buttonExecute.Size = New-Object System.Drawing.Size(100, 30)
$form.Controls.Add($buttonExecute)

# Función para crear copia de seguridad
function Create-Backup {
    if ($cancelRequested) { return }
    $backupPath = "C:\Backup_Windows"
    if (-not (Test-Path $backupPath)) {
        New-Item -ItemType Directory -Path $backupPath -Force
    }
    $timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
    $backupFile = Join-Path $backupPath "Backup_$timestamp.zip"
    Compress-Archive -Path "$env:USERPROFILE\Documents", "$env:USERPROFILE\Desktop", "$env:USERPROFILE\Downloads" -DestinationPath $backupFile
    [System.Windows.Forms.MessageBox]::Show("Copia de seguridad completada: $backupFile", "Información")
}

# Función para eliminar aplicaciones preinstaladas
function Remove-Bloatware {
    if ($cancelRequested) { return }
    $appsToRemove = @(
        "Microsoft.3DBuilder",
        "Microsoft.XboxGameCallableUI",
        "Microsoft.ZuneMusic",
        "Microsoft.ZuneVideo",
        "Microsoft.People",
        "Microsoft.BingNews",
        "Microsoft.MicrosoftSolitaireCollection"
    )
    foreach ($app in $appsToRemove) {
        if ($cancelRequested) { return }
        Get-AppxPackage -Name $app | Remove-AppxPackage -AllUsers -ErrorAction SilentlyContinue
    }
    [System.Windows.Forms.MessageBox]::Show("Aplicaciones preinstaladas eliminadas", "Información")
}

# Función para deshabilitar servicios
function Disable-Services {
    if ($cancelRequested) { return }
    $servicesToDisable = @(
        "DiagTrack", "dmwappushservice", "XboxGipSvc", "xbgm", "XblAuthManager", "XblGameSave"
    )
    foreach ($service in $servicesToDisable) {
        if ($cancelRequested) { return }
        Stop-Service -Name $service -Force -ErrorAction SilentlyContinue
        Set-Service -Name $service -StartupType Disabled
    }
    [System.Windows.Forms.MessageBox]::Show("Servicios innecesarios deshabilitados", "Información")
}

# Función para configurar privacidad
function Configure-Privacy {
    if ($cancelRequested) { return }
    Set-ItemProperty -Path "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection" -Name "AllowTelemetry" -Value 0
    Set-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Search" -Name "CortanaConsent" -Value 0
    [System.Windows.Forms.MessageBox]::Show("Ajustes de privacidad configurados", "Información")
}

# Función para limpiar almacenamiento
function Clean-Storage {
    if ($cancelRequested) { return }
    Start-Process -FilePath "cleanmgr.exe" -ArgumentList "/sagerun:1" -NoNewWindow -Wait
    [System.Windows.Forms.MessageBox]::Show("Espacio en disco liberado", "Información")
}

# Acción al presionar "Ejecutar"
$buttonExecute.Add_Click({
    if ($checkboxBackup.Checked) { Create-Backup }
    if ($checkboxBloatware.Checked) { Remove-Bloatware }
    if ($checkboxServices.Checked) { Disable-Services }
    if ($checkboxPrivacy.Checked) { Configure-Privacy }
    if ($checkboxClean.Checked) { Clean-Storage }

    if (-not $cancelRequested) {
        [System.Windows.Forms.MessageBox]::Show("Script completado. Reinicia tu sistema.", "Finalizado")
    }
    $form.Close()
})

# Mostrar la ventana
[void]$form.ShowDialog()
