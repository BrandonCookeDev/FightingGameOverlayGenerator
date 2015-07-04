﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SmashOverlayGeneratorMk2.General;

namespace SmashOverlayGeneratorMk2.ErrorHandling
{
    class ErrorHandler
    {
        #region ErrorHandling

        /*
        private bool dataFilledIn()
        {
            try
            {
                verifyTourneyDataFilled();
                verifyCombatantsFilled();
                verifyTournamentRoundFilled();
            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
                return false;
            }
            return true;
        }
        */

        public static void verifyCombatantsFilled(string GameType,
                                            Label singlesP1Label, Label singlesP2Label,
                                            Label singlesP1ScoreLabel, Label singlesP2ScoreLabel,
                                            TextBox singlesP1Textbox, TextBox singlesP2Textbox,
                                            TextBox singlesP1ScoreTextbox, TextBox singlesP2ScoreTextbox,
            
                                            Label doublesT1P1Label, Label doublesT1P2Label, Label doublesT2P1Label,
                                            Label doublesT2P2Label, Label doublesT1ScoreLabel, Label doublesT2ScoreLabel,
                                            TextBox doublesT1P1Textbox, TextBox doublesT1P2Textbox,
                                            TextBox doublesT2P1Textbox, TextBox doublesT2P2Textbox,
                                            TextBox doublesT1ScoreTextbox, TextBox doublesT2ScoreTextbox)
        {
            if (GameType.Equals("singles"))
            {

                singlesP1Label.ForeColor = Color.Black;
                singlesP2Label.ForeColor = Color.Black;
                singlesP1ScoreLabel.ForeColor = Color.Black;
                singlesP2ScoreLabel.ForeColor = Color.Black;

                if (GenFcns.isEmpty(singlesP1Textbox))
                {
                    singlesP1Label.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(singlesP1Textbox);
                    throw new Exception("Player 1 must have a name");
                }
                else if (GenFcns.isEmpty(singlesP2Textbox))
                {
                    singlesP2Label.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(singlesP2Textbox);
                    throw new Exception("Player 2 must have a name");
                }
                else if (GenFcns.isEmpty(singlesP1ScoreTextbox))
                {
                    singlesP1ScoreLabel.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(singlesP1ScoreTextbox);
                    throw new Exception("Player 1 must have a score");
                }
                else if (GenFcns.isEmpty(singlesP2ScoreTextbox))
                {
                    singlesP2ScoreLabel.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(singlesP2ScoreTextbox);
                    throw new Exception("Player 2 must have a score");
                }
            }
            else if (GameType.Equals("doubles"))
            {
                doublesT1P1Label.ForeColor = Color.Black;
                doublesT1P2Label.ForeColor = Color.Black;
                doublesT2P1Label.ForeColor = Color.Black;
                doublesT2P2Label.ForeColor = Color.Black;
                doublesT1ScoreLabel.ForeColor = Color.Black;
                doublesT2ScoreLabel.ForeColor = Color.Black;

                if (GenFcns.isEmpty(doublesT1P1Textbox))
                {
                    doublesT1P1Label.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(doublesT1P1Textbox);
                    throw new Exception("Team 1: Member 1 must have a name");
                }
                else if (GenFcns.isEmpty(doublesT1P2Textbox))
                {
                    doublesT1P2Label.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(doublesT1P2Textbox);
                    throw new Exception("Team 1: Member 2 must have a name");
                }
                else if (GenFcns.isEmpty(doublesT2P1Textbox))
                {
                    doublesT2P1Label.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(doublesT2P1Textbox);
                    throw new Exception("Team 2: Member 1 must have a name");
                }
                else if (GenFcns.isEmpty(doublesT2P2Textbox))
                {
                    doublesT2P2Label.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(doublesT2P2Textbox);
                    throw new Exception("Team 2: Member 2 must have a name");
                }
                else if (GenFcns.isEmpty(doublesT1ScoreTextbox))
                {
                    doublesT1ScoreLabel.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(doublesT1ScoreTextbox);
                    throw new Exception("Team 1 must have a score");
                }
                else if (GenFcns.isEmpty(doublesT2ScoreTextbox))
                {
                    doublesT2ScoreLabel.ForeColor = Color.Red;
                    GenFcns.putCursorInBox(doublesT2ScoreTextbox);
                    throw new Exception("Team 2 must have a score");
                }
            }
            else
                throw new Exception("An Error Occured" +
                     "\r\nIf Sinlges, only Singles section can contain text and all fields must be filled." +
                     "\r\nIf Doubles, only Doubles section can contain text and all fields must be filled. \r\n" +
                     "\r\nPlease correct any errors and try again. \r\nIf problem persists, contact cookiE.");
        }

        public static void verifyTournamentRoundFilled(ComboBox tournamentRoundCombobox, 
                                                 Label tournamentRoundLabel)
        {
            if (tournamentRoundCombobox.Text.Equals(""))
            {
                tournamentRoundLabel.ForeColor = Color.Red;
                tournamentRoundCombobox.Focus();
                tournamentRoundCombobox.Select(0, 0);
                throw new Exception("An Error Occured" +
                    "\r\nTournament Round not filled in." +
                    "\r\nPlease correct field and try again");
            }
            else
                tournamentRoundLabel.ForeColor = Color.Black;
        }

        public static void verifyTourneyDataFilled(string TemplateFile, Label tournamentNameLabel,
                                            TextBox tournamentNameTextbox, Button browseButton, 
                                            TabControl smashOverlayTabControl, TabPage tourneyDataTab)
        {
            tournamentNameLabel.ForeColor = Color.Black;
            browseButton.ForeColor = Color.Black;

            if (tournamentNameTextbox.Text.Equals(""))
            {
                smashOverlayTabControl.SelectedTab = tourneyDataTab;
                tournamentNameLabel.ForeColor = Color.Red;
                tournamentNameTextbox.Focus();
                tournamentNameTextbox.Select(0, 0);
                throw new Exception("An Error Occured" +
                    "\r\nTournament Name not completely filled in" +
                    "\r\nPlease correct fields and try again");
            }
            else if (TemplateFile == null)
            {
                smashOverlayTabControl.SelectedTab = tourneyDataTab;
                browseButton.ForeColor = Color.Red;
                browseButton.Focus();
                throw new Exception("An Error Occured" +
                    "\r\nImage Overlay not selected" +
                    "\r\nPlease select image and try again");
            }
        }

        public static void verifyCasterDataFilledIn(string CasterTemplateFile,
                                                    Label caster1Label, Label caster2Label,
                                                    CheckBox caster1Checkbox, CheckBox caster2Checkbox,
                                                    TextBox caster1Textbox, TextBox caster2Textbox,
                                                    TabControl smashOverlayTabControl,
                                                    TabPage casterTab)
        {
            caster1Label.ForeColor = Color.Black;
            caster2Label.ForeColor = Color.Black;

            if (!caster1Checkbox.Checked && !caster2Checkbox.Checked)
                throw new Exception("No Casters Selected");

            if (caster1Checkbox.Checked && caster1Textbox.Text.Equals(""))
            {
                smashOverlayTabControl.SelectedTab = casterTab;
                caster1Label.ForeColor = Color.Red;
                GenFcns.putCursorInBox(caster1Textbox);
                throw new Exception("Caster 1's Name cannot be blank");
            }


            if (caster2Checkbox.Checked && caster2Textbox.Text.Equals(""))
            {
                smashOverlayTabControl.SelectedTab = casterTab;
                caster2Label.ForeColor = Color.Red;
                GenFcns.putCursorInBox(caster2Textbox);
                throw new Exception("Caster 2's Name cannot be blank");
            }

            if (CasterTemplateFile == null || CasterTemplateFile.Equals(""))
            {
                throw new Exception("Caster Template File not selected!");
            }
        }

        public static void checkScoreFormat(string Score1, string Score2)
        {
            //CHECK SCORE ONE
            if (!GenFcns.checkIfDigits(Score1) || !GenFcns.checkIfDigits(Score2))
                throw new Exception("A score is formatted incorrectly" +
                    "\r\nPlease make sure that all scores are only numeric");
            else if (Int32.Parse(Score1) < 0 || Int32.Parse(Score1) > 100
                     || Int32.Parse(Score2) < 0 || Int32.Parse(Score2) > 100)
                throw new Exception("A score is not in the acceptable range" +
                    "\r\nPlease make sure that all scores are between 0 and 100");
        }
        #endregion ErrorHandling 
    }
}