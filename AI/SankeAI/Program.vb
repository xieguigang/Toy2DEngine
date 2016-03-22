﻿Imports Microsoft.VisualBasic.Parallel


Module Program

    Sub Main()

        Dim game = New Snake.Form1
        Call RunTask(AddressOf game.ShowDialog)
        Call Threading.Thread.Sleep(2000)

        Dim q As New QLAI(game.GameEngine)
        game.GameEngine.ControlsMap.Enable = False

        Call q.RunLearningLoop(200, Function(n) New QTable(n))

        Call LearnPlayGame.LearnPlay(5000)
        Pause()
    End Sub

End Module
